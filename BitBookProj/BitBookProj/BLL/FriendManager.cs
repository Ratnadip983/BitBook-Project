using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BitBookProj.DAL;
using BitBookProj.Models;

namespace BitBookProj.BLL
{
    public class FriendManager
    {

        FriendGateway _friendGateway=new FriendGateway();

        public bool sendFriendRequest(Friend friend)
        {
            return _friendGateway.sendFriendRequest(friend);
        }

        public bool IsAccepted(Friend friend)
        {
            return _friendGateway.IsAccepted(friend);
        }

        public Friend GetReceivedFriendById(int friendId,int userId)
        {
            return _friendGateway.GetReceivedFriendById(friendId, userId);
        }


        public bool RemoveFriend(Friend friend)
        {
            return _friendGateway.RemoveFriend(friend);
        }

        public Friend GetFriendById(int friendId, int userId)
        {
            return _friendGateway.GetFriendById(friendId, userId);
        }

        public bool AcceptFriend(Friend friend)
        {
            return _friendGateway.AcceptFriend(friend);
        }
        public List<User> SentRequest(User user)
        {
            return _friendGateway.SentRequest(user);
        }

        public List<User> ReceivedRequest(User user)
        {
            return _friendGateway.ReceivedRequest(user);
        }

        public List<User> FriendList(User user)
        {
            return _friendGateway.FriendList(user);
        }
    }
}