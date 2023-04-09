# ToTaL
The ToTaL Virtual Machine

## Commands
|Bytecode|AsmName|Args       |Description                                                                                                      |
|--------|-------|-----------|-----------------------------------------------------------------------------------------------------------------|
|10      |sta    |val        |Stores a value in register A.                                                                                    |
|11      |stb    |val        |Stores a value in register B.                                                                                    |
|12      |stc    |val        |Stores a value in register C.                                                                                    |
|13      |stx    |val        |Stores a value in register X.                                                                                    |
|14      |sty    |val        |Stores a value in register Y.                                                                                    |
|15      |stz    |val        |Stores a value in register Z.                                                                                    |
|1f      |mov    |addr,val   |Stores a value in a 24-bit memory address.                                                                       |
|20      |lda    |addr       |Loads a value from register A to a 24-bit memory address.                                                        |
|21      |ldb    |addr       |Loads a value from register B to a 24-bit memory address.                                                        |
|22      |ldc    |addr       |Loads a value from register C to a 24-bit memory address.                                                        |
|23      |ldx    |addr       |Loads a value from register X to a 24-bit memory address.                                                        |
|24      |ldy    |addr       |Loads a value from register Y to a 24-bit memory address.                                                        |
|25      |ldz    |addr       |Loads a value from register Z to a 24-bit memory address.                                                        |
|2f      |cpy    |addr1,addr2|Copies a value from one memory adress to another.                                                                |
|30      |srd    |addr       |Reads the byte at the cursor from the standard IO and saves it to a memory address.                              |
|31      |swt    |val        |Writes a byte to the standard IO. (DOESN'T INCREMENT CURSOR!)                                                    |
|32      |srm    |addr,val   |Reads a specified amount of bytes starting from the cursor from the standard IO and saves it to a memory address.|
|33      |swm    |string     |Writes a string to the standard IO at the cursor's position.                                                     |
|34      |spt    |pos        |Sets the standard IO cursor to a specified 16-bit position.                                                      |
|35      |spi    |           |Increments the standard IO cursor position.                                                                      |
|36      |spd    |           |Decrements the standard IO cursor position.                                                                      |
|40      |saa    |           |Sets register A as the active register.                                                                          |
|41      |sab    |           |Sets register B as the active register.                                                                          |
|42      |sac    |           |Sets register C as the active register.                                                                          |
|43      |sax    |           |Sets register X as the active register.                                                                          |
|44      |say    |           |Sets register Y as the active register.                                                                          |
|45      |saz    |           |Sets register Z as the active register.                                                                          |
|46      |laa    |           |Writes the value of the active register to register A.                                                           |
|47      |lab    |           |Writes the value of the active register to register B.                                                           |
|48      |lac    |           |Writes the value of the active register to register C.                                                           |
|49      |lax    |           |Writes the value of the active register to register X.                                                           |
|4a      |lay    |           |Writes the value of the active register to register Y.                                                           |
|4b      |laz    |           |Writes the value of the active register to register Z.                                                           |
|50      |inc    |           |Increments the active register.                                                                                  |
|51      |dec    |           |Decrements the active register.                                                                                  |
|52      |add    |val        |Adds a value to the active register.                                                                             |
|53      |sub    |val        |Subtracts a value to the active register.                                                                        |
|54      |mlt    |val        |Multiplies the active register's value by a specified value.                                                     |
|55      |div    |val        |Divides the active register's value by a specified value.                                                        |

