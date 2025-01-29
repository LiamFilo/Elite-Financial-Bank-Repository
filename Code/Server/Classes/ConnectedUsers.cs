using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using BankingCommunication;
using BankingEntities;

namespace Server {
	/// <summary>
	/// ConnectedUsers class manage the list of users which connect to the server and
	/// bring us the ability to add a new user, check if user connected and disconnect
	/// user from server.
	/// </summary>
	public class ConnectedUsers {

		private Dictionary<User,Connection> _connectionsByUser;

		public ConnectedUsers()
		{
			_connectionsByUser = new Dictionary<User,Connection>();
		}

		/// <summary>
		/// The function get user id and check if the user is connect to the server.
		/// </summary>
		/// <param name="userID"></param>
		public bool CheckIfAccountConnect(User user)
		{
			return _connectionsByUser.ContainsKey(user);
		}

		/// <summary>
		/// The function add user to the the list of connection users. The user must have a
		/// stable connection. It also check if the user is already exist.
		/// </summary>
		/// <param name="userToAdd"></param>
		/// <param name="connectionOfTheUser"></param>
		public void ConnectUser(User userToAdd, Connection connectionOfTheUser)
		{
			if (!_connectionsByUser.ContainsKey(userToAdd) && connectionOfTheUser.IsConnected)
				_connectionsByUser[userToAdd] = connectionOfTheUser;
			else
				throw new Exception($"Invalid User Connection for: {userToAdd.ToString}");
		}

		/// 
		/// <param name="userID"></param>
		public void DisconnectUser(User userToRemove)
		{
			if (_connectionsByUser.ContainsKey(userToRemove) && _connectionsByUser[userToRemove].IsConnected)
				_connectionsByUser.Remove(userToRemove);
			else
                throw new Exception($"Invalid User Disconnection for: {userToRemove.ToString}");
        }

		public Connection GetConnection(string userID)
		{
			foreach (User user in this._connectionsByUser.Keys)
				if(user.Id == userID)
					return _connectionsByUser[user];
			throw new Exception("The user id you give is not exist");
		}

	}

}