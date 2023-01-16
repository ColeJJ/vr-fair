using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProvider {
    public static GameLevel[] levels = new GameLevel[] {
            new GameLevel {
            name = "Level 1",
            targetRowConfigurations = new TargetRowConfiguration[] {
                new TargetRowConfiguration {
                    targetSpawnCount = 2,
                    colorDisplayCount = 0,
                    heavyTargetCount = 0,
                    heavyTargetSpawnCount = 0
                },
                new TargetRowConfiguration {
                    targetSpawnCount = 2,
                    colorDisplayCount = 0,
                    heavyTargetCount = 0,
                    heavyTargetSpawnCount = 0
                },
                new TargetRowConfiguration {
                    targetSpawnCount = 2,
                    colorDisplayCount = 0,
                    heavyTargetCount = 0,
                    heavyTargetSpawnCount = 0
                },
                new TargetRowConfiguration {
                    targetSpawnCount = 0,
                    colorDisplayCount = 0,
                    heavyTargetCount = 0,
                    heavyTargetSpawnCount = 0
                }
            }
        },
            new GameLevel {
                name = "Level 2",
                targetRowConfigurations = new TargetRowConfiguration[] {
                    new TargetRowConfiguration {
                        targetSpawnCount = 1,
                        colorDisplayCount = 0,
                        heavyTargetCount = 0,
                        heavyTargetSpawnCount = 0
                    },
                    new TargetRowConfiguration {
                        targetSpawnCount = 0,
                        colorDisplayCount = 0,
                        heavyTargetCount = 2,
                        heavyTargetSpawnCount = 1
                    },
                    new TargetRowConfiguration {
                        targetSpawnCount = 2,
                        colorDisplayCount = 1,
                        heavyTargetCount = 0,
                        heavyTargetSpawnCount = 0
                    },
                    new TargetRowConfiguration {
                        targetSpawnCount = 2,
                        colorDisplayCount = 1,
                        heavyTargetCount = 0,
                        heavyTargetSpawnCount = 0
                    }
                }
            },
            new GameLevel {
                name = "Level 3",
                targetRowConfigurations = new TargetRowConfiguration[] {
                    new TargetRowConfiguration {
                        targetSpawnCount = 0,
                        colorDisplayCount = 0,
                        heavyTargetCount = 0,
                        heavyTargetSpawnCount = 0
                    },
                    new TargetRowConfiguration {
                        targetSpawnCount = 2,
                        colorDisplayCount = 0,
                        heavyTargetCount = 0,
                        heavyTargetSpawnCount = 0
                    },
                    new TargetRowConfiguration {
                        targetSpawnCount = 0,
                        colorDisplayCount = 0,
                        heavyTargetCount = 2,
                        heavyTargetSpawnCount = 1
                    },
                    new TargetRowConfiguration {
                        targetSpawnCount = 2,
                        colorDisplayCount = 0,
                        heavyTargetCount = 0,
                        heavyTargetSpawnCount = 0
                    }
                }
            }
        };
}
