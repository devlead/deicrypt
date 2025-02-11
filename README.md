# deicrypt

`deicrypt` is a command-line tool designed for encoding and decoding files. Using using DEI-related terms padded by safe spaces. 

## Features

- **Encode Files**: Convert unencoded files into encoded files with an option to compress the output.
- **Decode Files**: Convert encoded files back to their original format with an option to decompress the output.

## Commands

### Encode Command

- **Command**: `encode`
- **Description**: Encodes a specified input file.
- **Usage**:
  - Basic: 
    ```
    deicrypt encode <inputFile> <outputFile>
    ```
  - With Compression (Brotli):
    ```
    deicrypt encode <inputFile> <outputFile> --compress
    ```

### Decode Command

- **Command**: `decode`
- **Description**: Decodes a specified encoded file.
- **Usage**:
  - Basic: 
    ```
    deicrypt decode <inputFile> <outputFile>
    ```
  - With Decompression (Brotli):
    ```
    deicrypt decode <inputFile> <outputFile> --decompress
    ```


## Getting Started

```bash
dotnet tool install --global deicrypt
```

### Example

#### Input
```
NSA
```

#### Output
```
Allyship            Pronouns            Bias                Allyship            Allyship            Gender              
```
