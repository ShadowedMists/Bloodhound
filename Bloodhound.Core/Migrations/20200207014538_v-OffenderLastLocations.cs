using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bloodhound.Core.Migrations
{
    public partial class vOffenderLastLocations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"create view v_OffenderLastLocation as
select
	ol.OffenderId,
	o.OffenderName,
	l.Latitude,
	l.Longitude,
	l.LocationTime
from (
	select
		OffenderId, 
		MAX(OffenderLocationId) OffenderLocationId 
	from OffenderLocations 
	group by OffenderId
) ol
inner join OffenderLocations l on ol.OffenderLocationId = l.OffenderLocationId
inner join Offenders o on ol.OffenderId = o.OffenderId");

            migrationBuilder.Sql(@"create procedure p_OffenderGeoFence_Delete (
	@OffenderGeoFenceId bigint
) as
begin
	delete from OffenderEvents where OffenderGeoFenceId = @OffenderGeoFenceId
	delete from OffenderGeoFences where OffenderGeoFenceId = @OffenderGeoFenceId
end");

            migrationBuilder.Sql(@"create procedure p_Offender_Delete (
	@OffenderId bigint
) as
begin
	delete from OffenderGeoFences where OffenderId = @OffenderId
	delete from OffenderLocations where OffenderId = @OffenderId
	delete from Offenders where OffenderId = @OffenderId
end");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("drop view v_OffenderLastLocation");
            migrationBuilder.Sql("drop procedure p_OffenderGeoFence_Delete");
            migrationBuilder.Sql("drop procedure p_Offender_Delete");
        }
    }
}
