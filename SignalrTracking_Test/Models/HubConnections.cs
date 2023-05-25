namespace SignalrTracking_Test.Models
{
    public class HubConnections
    {
        //public static Dictionary<string, List<string>> Users = new();
        public static Dictionary<string, UserConnection> Users = new();


        public static bool HasUserConnection(string UserId, string ConnectionId)
        {
            try
            {
                if (Users.ContainsKey(UserId))
                {
                    //return Users[UserId].Any(p => p.Contains(ConnectionId));
                    return Users[UserId].ConnectionIds.Contains(ConnectionId);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }

        public static bool HasUser(string UserId)
        {
            //try
            //{
            //    if (Users.ContainsKey(UserId))
            //    {
            //        return Users[UserId].Any();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}

            //return false;
            return Users.ContainsKey(UserId);


        }

        public static void AddUserConnection(string UserId, string ConnectionId, List<Vehicle> customizedVehicle)
        {

            //if (!string.IsNullOrEmpty(UserId) && !HasUserConnection(UserId, ConnectionId))
            //{
            //    if (Users.ContainsKey(UserId))
            //        Users[UserId].Add(ConnectionId);
            //    else
            //        Users.Add(UserId, new List<string> { ConnectionId });
            //}
            if (!HasUserConnection(UserId, ConnectionId))
            {
                if (HasUser(UserId))
                {
                    Users[UserId].ConnectionIds.Add(ConnectionId);
                }
                else
                {
                    var userConnection = new UserConnection(UserId, new List<string> { ConnectionId }, customizedVehicle);
                    Users.Add(UserId, userConnection);
                }
            }
        }

        public static List<string> OnlineUsers()
        {
            return Users.Keys.ToList();
        }
    }
}
