using Dapper;
using Lesson_DotNet_Dapper_Advanced.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lesson_DotNet_Dapper_Advanced.Entities.Vehicle;

namespace Lesson_DotNet_Dapper_Advanced.Repositories;

internal class VehicleRepository
{
    private readonly IDbConnection _dbConnection;

    public VehicleRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<IEnumerable<Vehicle>> GetByIdAsync(int vehicleId)
    {
        var queryCommand = @"
            SELECT 
	        veiculo.VeiculoID AS VehicleId,
	        veiculo.Placa AS Plate,
	        veiculo.DataCriacao AS CreateDate,
	        cor.CorID AS ColorId,
	        cor.Nome AS Name,
	        cor.DataCriacao AS CreateDate,
	        tipo.VeiculoTipoID AS TypeId,
	        tipo.Nome AS Name,
	        tipo.DataCriacao AS CreateDate
        FROM tblVeiculo veiculo
        INNER JOIN tblCor cor on cor.CorID = veiculo.CorID
        INNER JOIN tblVeiculoTipo tipo on tipo.VeiculoTipoID = veiculo.VeiculoTipoID
        WHERE 
            veiculo.VeiculoId = @vehicleId";

        return await _dbConnection.QueryAsync<Vehicle, VehicleColor, VehicleType, Vehicle>(
                    queryCommand,
                    (vehicle, vehicleColor, vehicleType) =>
                    {
                        vehicle.Color = vehicleColor;

                        vehicle.Type = vehicleType;

                        return vehicle;
                    },
                    splitOn: "ColorId,TypeId",
                    param: new
                    {
                        vehicleId
                    });
    }

    public async Task<IEnumerable<Vehicle>> GetAsync()
    {
        var queryCommand = @"
            SELECT 
	            veiculo.VeiculoID AS VehicleId,
	            veiculo.Placa AS Plate,
	            veiculo.DataCriacao AS CreateDate,
	            cor.CorID AS ColorId,
	            cor.Nome AS Name,
	            cor.DataCriacao AS CreateDate,
	            tipo.VeiculoTipoID AS TypeId,
	            tipo.Nome AS Name,
	            tipo.DataCriacao AS CreateDate
            FROM tblVeiculo veiculo
            INNER JOIN tblCor cor on cor.CorID = veiculo.CorID
            INNER JOIN tblVeiculoTipo tipo on tipo.VeiculoTipoID = veiculo.VeiculoTipoID";

        return await _dbConnection.QueryAsync<Vehicle, VehicleColor, VehicleType, Vehicle>(
                    queryCommand,
                    (Vehicle vehicle, VehicleColor vehicleColor, VehicleType vehicleType) =>
                    {
                        vehicle.Color = vehicleColor;

                        vehicle.Type = vehicleType;

                        return vehicle;
                    },
                    splitOn: "VehicleId,ColorId,TypeId");
    }
}
