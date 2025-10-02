# Todo

- Fix `RelicModServiceTests`'s use of obsolete class.
- Fix a number of relics not using 'amount' property, and instead 'quantity' on pattern.
  - BridleOfPegasusRespawn
  - GoldenLionsRespawn
  - RelicMonkeyRespawn
  - TailOfFeiRespawn
  - TuskOfDangkangSpawn
- Add missing relics to list of '_watchedTechs', cause they all add a effect that can/should be multiplied.
- Ensure relics that have 1+ untouched effects + 1+ touched effects, do not touch remove untouched effects.
