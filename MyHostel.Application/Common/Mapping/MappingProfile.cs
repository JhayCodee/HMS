﻿using AutoMapper;
using MyHostel.Application.Habitaciones.Commands.CrearHabitacion;
using MyHostel.Application.Habitaciones.Dtos;
using MyHostel.Application.Seguridad.Usuario.Dtos;
using MyHostel.Domain.Entities;
using MyHostel.Domain.Entities.Security;

namespace MyHostel.Application.Common.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CrearHabitacionCommand, Habitacion>();
        CreateMap<Habitacion, HabitacionDto>()
            .ForMember(dest => dest.Tipo,
                opt => opt.MapFrom(src => src.Tipo.ToString()))
            .ForMember(dest => dest.Estado,
                opt => opt.MapFrom(src => src.Estado.ToString()));


        #region SEGURIDAD

        CreateMap<Usuario, UsuarioDto>();

        #endregion

    }
}
