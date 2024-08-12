using AutoMapper;
using GEBIntegrador.Dominio;
using GEBIntegrador.Dto;

namespace GEBIntegrador.Infraestructura
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            #region Usuario
            CreateMap<usuario, UsuarioDto>()
                .ForMember(destino => destino.Perfil,
                opt => opt.MapFrom(origen => origen.n_id_perfilNavigation.v_descripcion))
                .ForMember(destino => destino.v_nombres,
                opt => opt.MapFrom(origen => origen.n_id_personaNavigation.v_nombres))
                .ForMember(destino => destino.v_apellidos,
                opt => opt.MapFrom(origen => origen.n_id_personaNavigation.v_apellidos)
                );

            CreateMap<UsuarioDto, usuario>()
                .ForMember(destino => destino.n_id_perfilNavigation,
                opt => opt.Ignore());

            CreateMap<usuario, UsuarioAutorizadorDto>()
                .ForMember(destino => destino.Perfil,
                opt => opt.MapFrom(origen => origen.n_id_perfilNavigation.v_descripcion))
                .ForMember(destino => destino.v_nombres,
                opt => opt.MapFrom(origen => origen.n_id_personaNavigation.v_nombres))
                .ForMember(destino => destino.v_apellidos,
                opt => opt.MapFrom(origen => origen.n_id_personaNavigation.v_apellidos)
                );

            CreateMap<UsuarioAutorizadorDto, usuario>()
                .ForMember(destino => destino.n_id_perfilNavigation,
                opt => opt.Ignore());

            CreateMap<usuario, UsuarioCrearDto>()
                .ForMember(destino => destino.Perfil,
                opt => opt.MapFrom(origen => origen.n_id_perfilNavigation.v_descripcion))
                .ForMember(destino => destino.persona,
                opt => opt.MapFrom(origen => origen.n_id_personaNavigation)
                );

            CreateMap<UsuarioCrearDto, usuario>()
                            //.ForMember(destino => destino.n_id_personaNavigation,
                            //opt => opt.MapFrom(origen => origen.persona))
                            .ForMember(destino => destino.n_id_persona,
                            opt => opt.MapFrom(origen => origen.persona.n_id));

            #endregion Usuario

            #region Perfiles
            CreateMap<perfile, PerfilDto>().ReverseMap();
            //CreateMap<Perfile, PerfilDto>()
            //    .ForMember(destino => destino.Perfil,
            //    opt => opt.MapFrom(origen => origen.n_id_perfilNavigation.v_descripcion)
            //    );

            //CreateMap<UsuarioDto, Usuario>()
            //    .ForMember(destino => destino.n_id_perfilNavigation,
            //    opt => opt.Ignore());
            #endregion Perfiles

            #region Persona
            CreateMap<persona, PersonaDto>().ReverseMap();
            #endregion Persona

            #region Parqueadero
            CreateMap<recurso, ParqueaderoDto>().ReverseMap();
            #endregion Parqueadero

            #region Recurso
            // CreateMap<recurso, RecursoDto>().ReverseMap();
            CreateMap<recurso, RecursoDto>()
                .ForMember(destino => destino.v_tipo_recurso,
                opt => opt.MapFrom(origen => origen.n_tipo_recursoNavigation.v_descripcion))
                .ForMember(destino => destino.v_nivel_recurso,
                opt => opt.MapFrom(origen => origen.n_nivel_recursoNavigation.v_descripcion));

            CreateMap<RecursoDto, recurso>()
                .ForMember(destino => destino.n_nivel_recursoNavigation,
                opt => opt.Ignore())
                .ForMember(destino => destino.n_tipo_recursoNavigation,
                opt => opt.Ignore());

            #endregion Recurso
            
            #region Reserva Parqueadero
            CreateMap<reserva, ReservaParqueaderoDto>().ReverseMap();
            #endregion Reserva Parqueadero

            #region Reserva Recurso
            CreateMap<reserva, ReservaSalaDto>().ReverseMap();

            CreateMap<reserva, ReservaRecursoDto>().ReverseMap();
            #endregion Reserva Recurso

            #region Niveles
            CreateMap<niveles, NivelesDto>().ReverseMap();
            CreateMap<NivelesDto, niveles>().ReverseMap();

            #endregion Niveles

        }
    }
}
