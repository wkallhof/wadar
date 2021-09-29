var map = null;

var source = new ol.source.ImageWMS({
    attributions: ['NOAA'],
    url: 'https://nowcoast.noaa.gov/arcgis/services/nowcoast/radar_meteo_imagery_nexrad_time/MapServer/WMSServer',
    params: {
        'LAYERS': '1'
    },
    projection: 'EPSG:3857'
});

var noaaLayer = new ol.layer.Image({
    title: 'NOAA Radar',
    zIndex: 1,
    visible: true,
    source: source,
    opacity: 0.7
});

var layers = [
    new ol.layer.Tile({
        source: new ol.source.OSM()
    }),

    noaaLayer
]

function Step(time)
{
    var date = new Date(time);
    layers[1].getSource().updateParams({'TIME': date.toISOString()});
}

function UpdateLocation(location)
{
    const view = map.getView();
    view.setCenter(ol.proj.fromLonLat([location.longitude, location.latitude]));
    view.setZoom(10);
}

function initializeMap(targetElementId, longitude, latitude, zoom){
    map = new ol.Map({
        target: targetElementId,
        layers : layers,
        view: new ol.View({
            center: ol.proj.fromLonLat([longitude, latitude]),
            zoom: zoom
        })
    });
}