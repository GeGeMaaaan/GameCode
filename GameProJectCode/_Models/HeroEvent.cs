using Gamee.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamee._Models
{
    public class HeroEvent
    {
        public delegate void HeroEventsDelegate(Hero hero);
        public Dictionary<string,Action> events;
        public Hero hero;
        public HeroEvent(Hero hero)
        {
            this.hero = hero;
            events = new Dictionary<string, Action>();
            InitializeEvents();
        }
        private void InitializeEvents()
        {
            events["PickMilitary"] = PickMilitary;
            events["PickDiplomat"] = PickDiplomat;
            events["PickPolitician"] = PickPolitician;
            events["HeroClimbDown"] = ClimbDown;
            events["HeroClimbUp"] = ClimbUp;
            events["ThirdFloorClimbUP"] = ClimbToThirdFloor;
            events["ThirdFloorClimbDown"] = ClimbDownToSecondFloor;
            events["PlayerNearHouse"] = PlayerNearHouse;
            events["EnterAHouseFirstTime"] = EnterAHouseFirstTime;

        }
        public void PickMilitary()
        {
            var MilitaryStats = new Dictionary<MainStats, int>();
            MilitaryStats[MainStats.dexterity] = 3;
            MilitaryStats[MainStats.strength] = 4;
            MilitaryStats[MainStats.intelligence] = 2;
            MilitaryStats[MainStats.gun_mastery] = 5;
            MilitaryStats[MainStats.medicine] = 3;
            MilitaryStats[MainStats.diplomacy] = 3;
            hero.StatsComponent.SetBaseStats(10, 2, 3, MilitaryStats, 0);
            hero.InventoryComponent.equipment.TryEquipLeft(new Revolver(hero.InventoryComponent, hero.InventoryComponent));
        }
        public void PickDiplomat()
        {
            var DiplomatStats = new Dictionary<MainStats, int>();
            DiplomatStats[MainStats.dexterity] = 2;
            DiplomatStats[MainStats.strength] = 3;
            DiplomatStats[MainStats.intelligence] = 4;
            DiplomatStats[MainStats.gun_mastery] = 3;
            DiplomatStats[MainStats.medicine] = 2;
            DiplomatStats[MainStats.diplomacy] = 6;
            hero.StatsComponent.SetBaseStats(10, 2, 3, DiplomatStats, 0);
        }
        public void PickPolitician()
        {
            var PoliticiannStats = new Dictionary<MainStats, int>();
            PoliticiannStats[MainStats.dexterity] = 2;
            PoliticiannStats[MainStats.strength] = 2;
            PoliticiannStats[MainStats.intelligence] = 6;
            PoliticiannStats[MainStats.gun_mastery] = 2;
            PoliticiannStats[MainStats.medicine] = 2;
            PoliticiannStats[MainStats.diplomacy] = 6;
            hero.StatsComponent.SetBaseStats(10, 2, 3, PoliticiannStats, 0);
        }
        public void ClimbUp()//Поднятия по лестнице вызывается в класс Ladder
        {
            hero.Position = new Vector2(800,  1000);
        }
        public void ClimbDown()//Опусканиия по лестнце вызвается в классе Ladder
        {
            hero.Position = new Vector2(600,1550);
        }
        public void ClimbToThirdFloor()
        {
            hero.Position = new Vector2(1230, 580);
        }
        public void ClimbDownToSecondFloor()
        {
            hero.Position = new Vector2(290, 1000);
        }
        public void PlayerNearHouse()
        {
            hero.subtitleManager.AddSubtitle("Странно... Дом выглядит не ухоженным. В детстве он был совершеннно другим");
        }
        public void EnterAHouseFirstTime()
        {
            hero.subtitleManager.AddSubtitle("Ого. Внутри дом выглядит намного лучше чем снаружи");
        }
    }
}
