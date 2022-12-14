using System.Linq;
using Application.Activities;
using Application.Comments;
using AutoMapper;
using Domain;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            string currentUsername = null;

            CreateMap<Activity, Activity>();
            CreateMap<Activity, ActivityDto>()
                .ForMember(d => d.HostUsername, o => o.MapFrom(s => s.Attendees
                    .FirstOrDefault(x => x.IsHost).AppUser.UserName));
            CreateMap<ActivityAttendee, AttendeeDto>()
                .ForMember(d => d.DisplayName, o => o.MapFrom(s => s.AppUser.DisplayName))
                .ForMember(d => d.Username, o => o.MapFrom(s => s.AppUser.UserName))
                .ForMember(d => d.Bio, o => o.MapFrom(s => s.AppUser.Bio))
                .ForMember(p => p.Image, opt => opt.MapFrom(s => s.AppUser.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(p => p.FollowersCount, o => o.MapFrom(s => s.AppUser.Followers.Count))
                .ForMember(p => p.FollowingCount, o => o.MapFrom(s => s.AppUser.Followings.Count))
                .ForMember(p => p.Following, o => o.MapFrom(x => x.AppUser.Followers.Any(f => f.Observer.UserName == currentUsername)));

            CreateMap<AppUser, Profiles.Profile>()
                .ForMember(p => p.Image, opt => opt.MapFrom(s => s.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(p => p.FollowersCount, o => o.MapFrom(s => s.Followers.Count))
                .ForMember(p => p.FollowingCount, o => o.MapFrom(s => s.Followings.Count))
                .ForMember(p => p.Following, o => o.MapFrom(x => x.Followers.Any(f => f.Observer.UserName == currentUsername)));
            
            CreateMap<Comment, CommentDto>()
                .ForMember(d => d.DisplayName, o => o.MapFrom(s => s.Author.DisplayName))
                .ForMember(d => d.Username, o => o.MapFrom(s => s.Author.UserName))
                .ForMember(p => p.Image, opt => opt.MapFrom(s => s.Author.Photos.FirstOrDefault(x => x.IsMain).Url));
        }
    }
}