using AutoMapper;

/// <summary>
/// Perfil de conversión entre <see cref="UserDTO"/> y <see cref="UserEntity"/> para
/// AutoMapper.
/// </summary>
internal class UserMapProfile : Profile
{
    public UserMapProfile()
    {
        /// Configura la conversión entre <see cref="UserDTO"/> u <see cref="UserEntity"/>.
        CreateMap<UserDTO, UserEntity>()
            /// Establece que la propiedad <see cref="UserDTO.email"/> corresponde a <see cref="UserEntity.emailAddress"/>.
            .ForMember(dst => dst.emailAddress, opt => opt.MapFrom(src => src.email))
            /// Establece que la propiedad <see cref="UserDTO.name"/> corresponde a <see cref="UserEntity.fullName"/>.
            .ForMember(dst => dst.fullName, opt => opt.MapFrom(src => src.name));
    }
}
