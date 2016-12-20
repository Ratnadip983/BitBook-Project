using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using BitBookProj.Models;

namespace BitBookProj.DAL
{
    public class FriendGateway
    {
        BitBookDbContext db=new BitBookDbContext();

        UserGateway _userGateway=new UserGateway();
        public bool sendFriendRequest(Friend friend)
        {
            db.Friends.Add(friend);
            return db.SaveChanges() > 0;
        }

        public bool IsAccepted(Friend friend)
        {
           Friend fnd = db.Friends.FirstOrDefault(c => ((c.User1Id == friend.User1Id && c.User2Id == friend.User2Id)||(c.User1Id==friend.User2Id && c.User2Id==friend.User1Id)));

            return fnd.IsAccepted;
        }

        public bool AcceptFriend(Friend friend)
        {
            db.Friends.Attach(friend);
            db.Entry(friend).State= EntityState.Modified;
            return db.SaveChanges() > 0;
        }

        public Friend GetReceivedFriendById(int friendId, int userId)
        {
            Friend friend = db.Friends.FirstOrDefault(c=>c.User1Id== friendId && c.User2Id==userId);

            return friend;
        }

        public Friend GetFriendById(int friendId,int userId)
        {
            Friend friend =
                db.Friends.FirstOrDefault(
                    c =>
                        (c.User1Id == friendId && c.User2Id == userId) ||
                        (c.User2Id == friendId && c.User1Id == userId));
            return friend;
        }

        public bool RemoveFriend(Friend friend)
        {
            db.Friends.Remove(friend);
            return db.SaveChanges() > 0;
        }

        public List<User> FriendList(User user)
        {
            List<Friend> fnds = db.Friends.Where(c => c.User1Id == user.Id || c.User2Id == user.Id).Where(c=>c.IsAccepted==true).ToList();
            List<User> friendList=new List<User>();

            foreach (Friend fnd in fnds)
            {
                if (fnd.User1Id != user.Id)
                {
                    friendList.Add(_userGateway.GetById(fnd.User1Id));
                }
                else
                {
                    friendList.Add(_userGateway.GetById(fnd.User2Id));
                }
            }

            return friendList;
        }

        public List<User> ReceivedRequest(User user)
        {
            List<Friend> fnds = db.Friends.Where(c => c.User2Id == user.Id).Where(c => c.IsAccepted == false).ToList();
            List<User> receivedList = new List<User>();

            foreach (Friend fnd in fnds)
            {
                receivedList.Add(_userGateway.GetById(fnd.User1Id));  
            }

            return receivedList;
        }

        public List<User> SentRequest(User user)
        {
            List<Friend> fnds = db.Friends.Where(c => c.User1Id == user.Id).Where(c => c.IsAccepted == false).ToList();
            List<User> sentList = new List<User>();

            foreach (Friend fnd in fnds)
            {
                sentList.Add(_userGateway.GetById(fnd.User2Id));
            }

            return sentList;
        }


    }
}