using KanBanApi.Implementation;

namespace KanBanApi.Drivers
{
    public class BoardRepository
    {
        public BoardDriver BoardDriver { get; set; }
        public BoardRepository(BoardDriver boardDriver)
        {
            BoardDriver = boardDriver;
            Initialize();
        }

        public void Initialize()
        {
            BoardDriver.WorkspaceList = () => Array.Empty<IDictionary<string, object>>();

            BoardDriver.AllBoards = () => Array.Empty<IDictionary<string, object>>();

            BoardDriver.BoardDetails = () => Array.Empty<IDictionary<string, object>>();

            BoardDriver.GetBoardsFromWorkspaceUUID =
            (repo, workspace) => repo
                .AllBoards()
                .Where(map => map.TryGetValue(BoardDriver.WorkspaceKey, out var val) && string.Equals(val, workspace))
                .ToArray();

            BoardDriver.GetBoardDetails =
            (driver, board) => Task.FromResult(driver
                .BoardDetails()
                .Where(map => map.TryGetValue(BoardDriver.BoardKey, out var val) && string.Equals(val, board)).FirstOrDefault());
        }
    }

    public class BoardDriver
    {
        public static string WorkspaceKey = "Workspace_UUID";
        public static string BoardKey = "Board_UUID";

        public Func<IDictionary<string, object>[]> WorkspaceList = () => Array.Empty<IDictionary<string, object>>();

        public Func<IDictionary<string, object>[]> AllBoards = () => Array.Empty<IDictionary<string, object>>();

        public Func<IDictionary<string, object>[]> BoardDetails = () => Array.Empty<IDictionary<string, object>>();

        public Func<BoardDriver, string, IDictionary<string, object>[]> GetBoardsFromWorkspaceUUID =
            (repo, workspace) =>
                Enumerable.Empty<IDictionary<string,object>>()
                .ToArray();

        public Func<BoardDriver, string, Task<IDictionary<string, object>?>> GetBoardDetails = 
            async (_,_) => await Task.FromResult(new Dictionary<string, object>());

        public IEnumerable<IDictionary<string, object>> GetWorkspaces()
        {
            foreach(var workspace in WorkspaceList())
            {
                yield return workspace;
            }
        }

        public IEnumerable<IDictionary<string, object>> GetBoardsByWorkspace(params string[] WorkspaceUUIDs)
        {
            foreach(var Workspace in WorkspaceUUIDs)
            {
                var Boards = GetBoardsFromWorkspaceUUID(this, Workspace);
                var BoardCount = Boards.Length;

                yield return new Dictionary<string,object>()
                {
                    { "Workspace", Workspace },
                    { "BoardCount", BoardCount },
                    { "Boards", Boards },
                };
            }
        }

        public async Task<IDictionary<string, object>[]> GetBoardDetailsAsync(params string[] BoardUUIDs)
        {
            BoardUUIDs = BoardUUIDs.Where(a => a != null).ToArray();

            var items = new IDictionary<string, object>[BoardUUIDs.Length];

            await Task.WhenAll(items.Select(async (input, index) => 
            {
                var result = await GetBoardDetails(this, BoardUUIDs[index]);
                items[index] = result!;
            }));

            return items;
        }
    }
}
