/* using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using E.Deezer;
using E.Deezer.Api;

namespace Deezcord
{
    class DeezerAPINew
    {
        public Deezer deezer;
        public static int currentlyPlaying;

        public async Task Login()
        {
            Deezer deezer = DeezerSession.CreateNew();
            await deezer.Login(DeezerAPI.token);
        }

        public async Task Start()
        {
            var history = deezer.User.GetHistory(0, 1);
            if (currentlyPlaying != history.Id)
            {

            }
            uint historyUint = Convert.ToUInt32(history.Id);
            var track = deezer.Browse.GetTrackById(historyUint);
        }
    }
}
*/