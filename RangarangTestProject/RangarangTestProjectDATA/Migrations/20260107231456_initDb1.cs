using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RangarangTestProjectDATA.Migrations
{
    /// <inheritdoc />
    public partial class initDb1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductGroupId = table.Column<int>(type: "int", nullable: false),
                    WorkTypeId = table.Column<int>(type: "int", nullable: false),
                    ProductType = table.Column<byte>(type: "tinyint", nullable: false),
                    Circulation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CopyCount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PageCount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrintSide = table.Column<byte>(type: "tinyint", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    IsCalculatePrice = table.Column<bool>(type: "bit", nullable: false),
                    IsCustomCirculation = table.Column<bool>(type: "bit", nullable: false),
                    IsCustomSize = table.Column<bool>(type: "bit", nullable: false),
                    IsCustomPage = table.Column<bool>(type: "bit", nullable: false),
                    MinCirculation = table.Column<int>(type: "int", nullable: true),
                    MaxCirculation = table.Column<int>(type: "int", nullable: true),
                    MinPage = table.Column<int>(type: "int", nullable: true),
                    MaxPage = table.Column<int>(type: "int", nullable: true),
                    MinWidth = table.Column<float>(type: "real", nullable: true),
                    MaxWidth = table.Column<float>(type: "real", nullable: true),
                    MinLength = table.Column<float>(type: "real", nullable: true),
                    MaxLength = table.Column<float>(type: "real", nullable: true),
                    SheetDimensionId = table.Column<int>(type: "int", nullable: false),
                    FileExtension = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsCmyk = table.Column<bool>(type: "bit", nullable: false),
                    CutMargin = table.Column<float>(type: "real", nullable: false),
                    PrintMargin = table.Column<float>(type: "real", nullable: false),
                    IsCheckFile = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TokenValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RevokedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NationalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProductAdts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    AdtId = table.Column<int>(type: "int", nullable: false),
                    Required = table.Column<bool>(type: "bit", nullable: false),
                    Side = table.Column<byte>(type: "tinyint", nullable: true),
                    Count = table.Column<int>(type: "int", nullable: true),
                    IsJeld = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAdts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductAdts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductDelivers",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsIncreased = table.Column<bool>(type: "bit", nullable: false),
                    StartCirculation = table.Column<int>(type: "int", nullable: false),
                    EndCirculation = table.Column<int>(type: "int", nullable: false),
                    PrintSide = table.Column<byte>(type: "tinyint", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    CalcType = table.Column<byte>(type: "tinyint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDelivers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductDelivers_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductMaterials",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    MaterialId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsJeld = table.Column<bool>(type: "bit", nullable: false),
                    Required = table.Column<bool>(type: "bit", nullable: false),
                    IsCustomCirculation = table.Column<bool>(type: "bit", nullable: false),
                    IsCombinedMaterial = table.Column<bool>(type: "bit", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductMaterials", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductMaterials_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductPrintKinds",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    PrintKindId = table.Column<int>(type: "int", nullable: false),
                    IsJeld = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPrintKinds", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductPrintKinds_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductSizes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Length = table.Column<float>(type: "real", nullable: false),
                    Width = table.Column<float>(type: "real", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SheetCount = table.Column<int>(type: "int", nullable: true),
                    SheetDimensionId = table.Column<int>(type: "int", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSizes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductSizes_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductAdtTypes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductAdtId = table.Column<int>(type: "int", nullable: false),
                    AdtTypeId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAdtTypes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductAdtTypes_ProductAdts_ProductAdtId",
                        column: x => x.ProductAdtId,
                        principalTable: "ProductAdts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductMaterialAttributes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductMaterialId = table.Column<int>(type: "int", nullable: false),
                    MaterialAttributeId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductMaterialAttributes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductMaterialAttributes_ProductMaterials_ProductMaterialId",
                        column: x => x.ProductMaterialId,
                        principalTable: "ProductMaterials",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductDeliverSizes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductDeliverId = table.Column<int>(type: "int", nullable: false),
                    ProductSizeId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDeliverSizes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductDeliverSizes_ProductDelivers_ProductDeliverId",
                        column: x => x.ProductDeliverId,
                        principalTable: "ProductDelivers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductDeliverSizes_ProductSizes_ProductSizeId",
                        column: x => x.ProductSizeId,
                        principalTable: "ProductSizes",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ProductPrices",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Circulation = table.Column<int>(type: "int", nullable: false),
                    IsDoubleSided = table.Column<bool>(type: "bit", nullable: false),
                    PageCount = table.Column<int>(type: "int", nullable: true),
                    CopyCount = table.Column<int>(type: "int", nullable: true),
                    ProductSizeId = table.Column<int>(type: "int", nullable: false),
                    ProductMaterialId = table.Column<int>(type: "int", nullable: false),
                    ProductMaterialAttributeId = table.Column<int>(type: "int", nullable: true),
                    ProductPrintKindId = table.Column<int>(type: "int", nullable: false),
                    IsJeld = table.Column<bool>(type: "bit", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPrices", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductPrices_ProductMaterialAttributes_ProductMaterialAttributeId",
                        column: x => x.ProductMaterialAttributeId,
                        principalTable: "ProductMaterialAttributes",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ProductPrices_ProductMaterials_ProductMaterialId",
                        column: x => x.ProductMaterialId,
                        principalTable: "ProductMaterials",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ProductPrices_ProductPrintKinds_ProductPrintKindId",
                        column: x => x.ProductPrintKindId,
                        principalTable: "ProductPrintKinds",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ProductPrices_ProductSizes_ProductSizeId",
                        column: x => x.ProductSizeId,
                        principalTable: "ProductSizes",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ProductPrices_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "ProductAdtPrices",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductAdtId = table.Column<int>(type: "int", nullable: false),
                    ProductPriceId = table.Column<int>(type: "int", nullable: false),
                    ProductAdtTypeId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAdtPrices", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductAdtPrices_ProductAdtTypes_ProductAdtTypeId",
                        column: x => x.ProductAdtTypeId,
                        principalTable: "ProductAdtTypes",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ProductAdtPrices_ProductAdts_ProductAdtId",
                        column: x => x.ProductAdtId,
                        principalTable: "ProductAdts",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_ProductAdtPrices_ProductPrices_ProductPriceId",
                        column: x => x.ProductPriceId,
                        principalTable: "ProductPrices",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductAdtPrices_ID",
                table: "ProductAdtPrices",
                column: "ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductAdtPrices_ProductAdtId",
                table: "ProductAdtPrices",
                column: "ProductAdtId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAdtPrices_ProductAdtTypeId",
                table: "ProductAdtPrices",
                column: "ProductAdtTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAdtPrices_ProductPriceId",
                table: "ProductAdtPrices",
                column: "ProductPriceId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAdts_ID",
                table: "ProductAdts",
                column: "ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductAdts_ProductId",
                table: "ProductAdts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAdtTypes_ID",
                table: "ProductAdtTypes",
                column: "ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductAdtTypes_ProductAdtId",
                table: "ProductAdtTypes",
                column: "ProductAdtId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDelivers_ID",
                table: "ProductDelivers",
                column: "ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductDelivers_ProductId",
                table: "ProductDelivers",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDeliverSizes_ProductDeliverId",
                table: "ProductDeliverSizes",
                column: "ProductDeliverId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDeliverSizes_ProductSizeId",
                table: "ProductDeliverSizes",
                column: "ProductSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductMaterialAttributes_ID",
                table: "ProductMaterialAttributes",
                column: "ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductMaterialAttributes_ProductMaterialId",
                table: "ProductMaterialAttributes",
                column: "ProductMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductMaterials_ID",
                table: "ProductMaterials",
                column: "ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductMaterials_ProductId",
                table: "ProductMaterials",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrices_ID",
                table: "ProductPrices",
                column: "ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrices_ProductID",
                table: "ProductPrices",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrices_ProductMaterialAttributeId",
                table: "ProductPrices",
                column: "ProductMaterialAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrices_ProductMaterialId",
                table: "ProductPrices",
                column: "ProductMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrices_ProductPrintKindId",
                table: "ProductPrices",
                column: "ProductPrintKindId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrices_ProductSizeId",
                table: "ProductPrices",
                column: "ProductSizeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrintKinds_ProductId",
                table: "ProductPrintKinds",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ID",
                table: "Products",
                column: "ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductSizes_ID",
                table: "ProductSizes",
                column: "ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductSizes_ProductId",
                table: "ProductSizes",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "ProductAdtPrices");

            migrationBuilder.DropTable(
                name: "ProductDeliverSizes");

            migrationBuilder.DropTable(
                name: "Tokens");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ProductAdtTypes");

            migrationBuilder.DropTable(
                name: "ProductPrices");

            migrationBuilder.DropTable(
                name: "ProductDelivers");

            migrationBuilder.DropTable(
                name: "ProductAdts");

            migrationBuilder.DropTable(
                name: "ProductMaterialAttributes");

            migrationBuilder.DropTable(
                name: "ProductPrintKinds");

            migrationBuilder.DropTable(
                name: "ProductSizes");

            migrationBuilder.DropTable(
                name: "ProductMaterials");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
