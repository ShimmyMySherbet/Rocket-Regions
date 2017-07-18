﻿using System.Collections.Generic;
using Rocket.Unturned.Player;
using RocketRegions.Util;
using SDG.Unturned;
using UnityEngine;

namespace RocketRegions.Model.Flag.Impl
{
    public class NoVehiclesUsageFlag : BoolFlag
    {
        public override string Description => "Allow/Disallow usage of vehicles in region";

        private readonly Dictionary<ulong, bool> _lastVehicleStates = new Dictionary<ulong, bool>();

        public override void UpdateState(List<UnturnedPlayer> players)
        {
            foreach (var player in players)
            {
                var id = PlayerUtil.GetId(player);
                var veh = player.Player.movement.getVehicle();
                var isInVeh = veh != null;

                if (!_lastVehicleStates.ContainsKey(id))
                    _lastVehicleStates.Add(id, veh);

                var wasDriving = _lastVehicleStates[id];

                if (!isInVeh || wasDriving ||
                    !GetValueSafe(Region.GetGroup(player))) continue;

                byte seat;
                Vector3 point;
                byte angle;
                veh.forceRemovePlayer(out seat, PlayerUtil.GetCSteamId(player), out point, out angle);
            }
        }

        public override void OnRegionEnter(UnturnedPlayer p)
        {
            //do nothing
        }

        public override void OnRegionLeave(UnturnedPlayer p)
        {
            //do nothing
        }
    }
}