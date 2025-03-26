open System

/// Generates a map with the specified width and height.
/// Parameters:
/// - width: int - The width of the map.
/// - height: int - The height of the map.
/// Returns:
/// - char[,]: 2D array representing the map.
let generateMap (width: int) (height: int) : char[,] =
    let map = Array2D.create width height '.'

    for y in 0 .. height - 1 do
        for x in 0 .. width - 1 do
            map.[x, y] <- '.'

    map

/// Prints the contents of a map.
/// Parameters:
/// - map: char[,] - The map to be printed.
let printMap (map: char[,]) =
    for y in 0 .. Array2D.length2 map - 1 do
        for x in 0 .. Array2D.length1 map - 1 do
            printf "%c" map.[x, y]
        Console.WriteLine()

/// Replaces symbols in the map with the specified choice symbol.
/// Parameters:
/// - map: char[,] - The map to be modified.
/// - choice: int - The number of symbols to replace.
/// - symbol: char - The replacement symbol.
/// Returns:
/// - char[,]: The modified map.
let replaceSymbols (map: char[,]) (choice: int) (symbol: char) : char[,] =
    let random = Random()

    for _ in 1 .. choice do
        let x = random.Next(0, Array2D.length1 map)
        let y = random.Next(0, Array2D.length2 map)
        map.[x, y] <- symbol

    map

/// Generates water blocks in the map by replacing '0' symbols with oval blocks.
/// Parameters:
/// - map: char[,] - The map to be modified.
/// - choice: int - The number of water blocks to generate.
/// Returns:
/// - char[,]: The modified map with water blocks.
let generateWaterBlocks (map: char[,]) (choice: int) : char[,] =
    let random = Random()

    for _ in 1..choice do
        let blockWidth = random.Next(2, 10) // Adjust the range of the width as desired
        let blockHeight = random.Next(2, 10) // Adjust the range of the height as desired
        let x = random.Next(blockWidth, Array2D.length1 map - blockWidth)
        let y = random.Next(blockHeight, Array2D.length2 map - blockHeight)

        for i in -blockWidth .. blockWidth do
            for j in -blockHeight .. blockHeight do
                if (i * i * blockHeight * blockHeight) + (j * j * blockWidth * blockWidth) <= (blockWidth * blockWidth * blockHeight * blockHeight) then
                    map.[x + i, y + j] <- '^' // water block

    map

/// Generates water blocks in the map,
/// replacing '0' elements with oval blocks
/// Parameters:
///     map: char[,] - Map to be modified
///     choice: int Number of water blocks
let generateWaterBlocksManyInstances (map: char[,]) (choice: int) =
    let wrInst = Random()
    let hrInst = Random()
    let bWidth: int = wrInst.Next (2, 10)
    let bHeight: int = hrInst.Next (2, 10)
    
    let bSHeight = bHeight * bHeight
    let bSWidth = bWidth * bWidth
    
    for _ in 1..choice do
        let x = wrInst.Next (wrInst.Next (2, 10), Array2D.length1 map - wrInst.Next(2, 10))
        let y = hrInst.Next (hrInst.Next (2, 10), Array2D.length2 map - hrInst.Next(2, 10))

        for i in -bWidth .. bWidth do
            for j in -bHeight .. bHeight do
                if (i * i * bSHeight) + (j * j * bSWidth) <= (bSHeight * bSWidth) then
                    map.[x + i, y + j] <- '^' // water block
    
    map

/// Generates tree blocks on the map by replacing '0' symbols.
/// Parameters:
/// - map: char[,] - The map to be modified.
/// - choice: int - The number of tree blocks to generate.
/// Returns:
/// - char[,]: The modified map with tree blocks.
let generateTreeBlocks (map: char[,]) (choice: int) : char[,] =
    for _ in 1 .. choice do
        let x = Random.Shared.Next(0, Array2D.length1 map)
        let y = Random.Shared.Next(0, Array2D.length2 map)

        map.[x, y] <- 'T'
    map
    
let compress unit =
    ()

[<EntryPoint>]
let main argv =
    let width = 230
    let height = 50

    let map = generateMap width height

    let newMapTrees = generateTreeBlocks map 300

    let newMapWater = generateWaterBlocks newMapTrees 15

    printMap newMapWater

    0