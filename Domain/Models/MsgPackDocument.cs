using Domain.Entities;
using MessagePack;

namespace Domain.Models
{
    [MessagePackObject]
    public class MsgPackDocument
    {
        [Key(0)]
        public Guid Id { get; set; }

        [Key(1)]
        public List<string> Tags { get; set; } = new List<string>();

        [IgnoreMember] // Investigate implementation of custom resolver
        public DocumentData Data { get; set; }
    }
}
