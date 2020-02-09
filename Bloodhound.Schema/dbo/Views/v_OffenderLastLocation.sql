create view v_OffenderLastLocation as
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
inner join Offenders o on ol.OffenderId = o.OffenderId