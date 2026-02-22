using AutoMapper;
using ch4rniauski.LinkShortener.Application.Dto.ShortLink.Requests;
using ch4rniauski.LinkShortener.Domain.Entities;

namespace ch4rniauski.LinkShortener.Application.MapperProfiles.ShortLink;

internal sealed class ShortLinkEntityProfile : Profile
{
    public ShortLinkEntityProfile()
    {
        CreateMap<ShortTheLinkRequestDto, ShortLinkEntity>()
            .ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(
                dest => dest.ClickCount,
                opt => opt.MapFrom(_ => 0))
            .ForMember(
                dest => dest.CreatedAt,
                opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(
                dest => dest.ShortToken,
                opt => opt.Ignore())
            .ForMember(
                dest => dest.OriginalUrl,
                opt => opt.MapFrom(src => src.OriginalUrl));
    }
}
