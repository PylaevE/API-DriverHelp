using NetTopologySuite.Geometries;
using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;

namespace RoadAPI.Helpers
{
    static class GeometryExtensions
    {
        public static double MetersDistance(this Point pointA, Point pointB)
        {
            var transformationFactory = new CoordinateTransformationFactory();
            var originCoordinateSystem = GeographicCoordinateSystem.WGS84;
            var targetCoordinateSystem = GeocentricCoordinateSystem.WGS84;

            var transform = transformationFactory.CreateFromCoordinateSystems(
                originCoordinateSystem, targetCoordinateSystem);

            var pointACoordinate = new GeoAPI.Geometries.Coordinate(pointA.X , pointA.Y);
            var pointBCoordinate = new GeoAPI.Geometries.Coordinate(pointB.X, pointB.Y);

            var newPointA = transform.MathTransform.Transform(pointACoordinate);
            var newPointB = transform.MathTransform.Transform(pointBCoordinate);

            return newPointB.Distance(newPointA) * 1.11;
        }
    }
}