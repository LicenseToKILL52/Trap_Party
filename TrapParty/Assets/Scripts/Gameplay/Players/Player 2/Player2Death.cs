﻿using System.Collections;
using System.Collections.Generic;
using Platformer.Core;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the player has died.
    /// </summary>
    /// <typeparam name="Player2Death"></typeparam>
    public class Player2Death : Simulation.Event<Player2Death>
    {
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            var player2 = model.player2;

            if (player2.health.IsAlive)
            {
                player2.health.Die();
                model.virtualCamera.m_Follow = null;
                model.virtualCamera.m_LookAt = null;
                //player.GetComponent<Collider>().enabled = false;
                player2.controlEnabled = false;

                if (player2.audioSource && player2.ouchAudio)
                    player2.audioSource.PlayOneShot(player2.ouchAudio);
                player2.animator.SetTrigger("hurt");
                player2.animator.SetBool("dead", true);
                Simulation.Schedule<Player2Spawn>(2);
            }
        }
    }
}