using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TareasMVC.Migrations
{
    /// <inheritdoc />
    public partial class AdminRol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            IF NOT EXISTS (SELECT Id FROM AspNetRoles WHERE Id = '8b167156-5bbc-4a5a-9e32-4f4a442def27')
            BEGIN 

                INSERT AspNetRoles(Id, [Name], [NormalizedName]) VALUES 
                ('8b167156-5bbc-4a5a-9e32-4f4a442def27', 'admin', 'ADMIN');	
                
            END
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE AspNetRoles WHERE Id = '8b167156-5bbc-4a5a-9e32-4f4a442def27'");
        }
    }
}
