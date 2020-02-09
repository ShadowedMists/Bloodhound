create procedure p_OffenderGeoFence_Delete (
	@OffenderGeoFenceId bigint
) as
begin
	delete from OffenderEvents where OffenderGeoFenceId = @OffenderGeoFenceId
	delete from OffenderGeoFences where OffenderGeoFenceId = @OffenderGeoFenceId
end