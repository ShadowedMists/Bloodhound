create procedure p_Offender_Delete (
	@OffenderId bigint
) as
begin
	delete from OffenderGeoFences where OffenderId = @OffenderId
	delete from OffenderLocations where OffenderId = @OffenderId
	delete from Offenders where OffenderId = @OffenderId
end