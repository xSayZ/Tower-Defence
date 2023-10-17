using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TD.Entity;
using System;

namespace TD {

    namespace Core {

        public class GameManager : MonoBehaviour {
            // Members
            public static GameManager instance;
            public bool debug;

            [Header("Round Settings")]
            [SerializeField] private List<RoundScriptableObject> rounds;
            [SerializeField] private Transform spawnPosition;
            private List<EnemyBehaviour> activeEnemies;

            private int currentRoundIndex = 0;
            private bool isRoundActive = false;
            private int TotalEnemies => activeEnemies.Count;

#region Unity Functions
            private void Awake() {
                if(!instance) {
                    Configure();
                }
            }
            void Start() {
                StartRound();
            }
            void Update() {

                if (isRoundActive) {
                    if (AllEnemiesDefeated()) {
                        EndRound();
                    }
                }
            }
#endregion

#region  Public Functions

            public static GameManager GetInstance() {
                return instance;
            }

            public void RegisterEnemy(EnemyBehaviour enemy) {
                activeEnemies.Add(enemy);
            }

            public void UnregisterEnemy(EnemyBehaviour enemy) {
                activeEnemies.Remove(enemy);
            }

#endregion

#region  Private Functions

            private void Configure(){
                instance = this;
                activeEnemies = new List<EnemyBehaviour>();
            }

            private void StartRound() {
                if (currentRoundIndex < rounds.Count) {
                    Log("Starting round: ["+(currentRoundIndex + 1) +"].");
                    isRoundActive = true;
                    StartCoroutine(SpawnRoundEnemies(rounds[currentRoundIndex]));
                }
            }
            private void EndRound() {
                isRoundActive = false;
                currentRoundIndex++;

                if (currentRoundIndex < rounds.Count) {
                    StartRound();
                }
            }

            private IEnumerator SpawnRoundEnemies(RoundScriptableObject round)
            {
                for (int i = 0; i < round.enemies.Count; i++) {
                    GameObject enemy = Instantiate(round.enemies[i], spawnPosition);
                    yield return new WaitForSeconds(round.spawnTime);
                }
            }
            bool AllEnemiesDefeated() {
                return TotalEnemies == 0;
            }

            private void Log(string _msg) {
                if(!debug) return;
                Debug.Log("[Game Manager]: "+_msg);
            }

            private void LogWarning(string _msg) {
                if(!debug) return;
                Debug.LogWarning("[Game Manager]: "+_msg);
            }
            #endregion
        }
    }   
}