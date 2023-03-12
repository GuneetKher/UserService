using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UserService.Models
{
    public class User
    {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("username")]
    public string Username { get; set; }

    [BsonElement("email")]
    public string Email { get; set; }

    [BsonElement("role")]
    public string Role { get; set; }

    [BsonElement("passwordhash")]
    public byte[] PasswordHash { get; set; }
    [BsonElement("passwordsalt")]
    public byte[] PasswordSalt { get; set; }
    }    
}