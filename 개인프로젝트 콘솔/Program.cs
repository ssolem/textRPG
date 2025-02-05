using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace 개인프로젝트_콘솔
{
    internal class Program
    {
        interface ICharacter
        {
            string Name { get; set; }
            string Job { get; set; }
            int Level { get; set; }
            int Attack { get; set; }
            int Defence { get; set; }
            int Health { get; set; }

            int Gold { get; set; }

        }

        public class Character : ICharacter
        {
            public string Name { get; set; }
            public string Job { get; set; }
            public int Level { get; set; }
            public int Attack { get; set; }
            public int Defence { get; set; }
            public int Health { get; set; }

            public int Gold { get; set; }

            public Character(string name, string job)
            {
                Name = name;
                Job = job;
                Level = 1;
                Attack = 10;
                Defence = 5;
                Health = 100;
                Gold = 4500;
            }

        }

        interface IItem
        {
            string ItemName { get; set; }

            bool Purchased { get; set; }
            int Price { get; set; }
            string Description { get; set; }

            bool Equipted { get; set; }
            public int Figure { get; set; }


            void Wear(Character character);
            void TakeOff(Character character);
            void Purchase(Character character);


        }

        public class AttackItem : IItem
        {
            public string ItemName { get; set; }
            public int Figure { get; set; }  
            public bool Purchased { get; set; }
            public int Price { get; set; }
            public string Description { get; set; }
            public bool Equipted { get; set; }

            




            public AttackItem(string name, int attack, int price, string description)
            {
                ItemName = name;
                Figure = attack;
                Purchased = false;
                Price = price;
                Description = description;
                Equipted = false;
            }

            public void Wear(Character character)
            {
                ItemName = "[E]" + ItemName;
                character.Attack += Figure;
                Equipted = true;
            }
            public void TakeOff(Character character)
            {
                ItemName = ItemName.Replace("[E]", "");
                character.Attack -= Figure;
                Equipted = false;
            }

            public void Purchase(Character character)
            {
                character.Gold -= Price;
                Purchased = true;
            }

        }

        public class Weapon : AttackItem
        {
            public Weapon(string name, int attack, int price, string description) : base(name, attack, price,description) { }
        }


        public class DefenceItem : IItem
        {
            public string ItemName { get; set; }
            public int Figure { get; set; }
            public bool Purchased { get; set; }
            public int Price { get; set; }
            public string Description { get; set; }
            public bool Equipted { get; set; }



            public DefenceItem(string name, int defence, int price, string description)
            {
                ItemName = name;
                Figure = defence;
                Purchased = false;
                Price = price;
                Description = description;
                Equipted = false;
            }

            public void Wear(Character character)
            {
                ItemName = "[E]" + ItemName;
                character.Defence += Figure;
                Equipted = true;
            }
            public void TakeOff(Character character)
            {
                ItemName = ItemName.Replace("[E]", "");
                character.Defence -= Figure;
                Equipted = false;
            }

            public void Purchase(Character character)
            {
                character.Gold -= Price;
                Purchased = true;
            }

        }
        public class Armor : DefenceItem
        {
            public Armor(string name, int attack, int price, string description) : base(name, attack, price, description) { }
        }


        static void Main(string[] args)
        {
            // ICharacter player = new Character("Chad", "전사"); 로 하려다가 오류남
            Character player = new Character("Chad", "전사");
            IItem weapon1 = new Weapon("낡은 검", 2, 600, "쉽게 볼 수 있는 낡은 검 입니다.");
            IItem weapon2 = new Weapon("청동 도끼", 5, 1500, "어디선가 사용됐던거 같은 도끼입니다.");
            IItem weapon3 = new Weapon("스파르타의 창", 7, 2000, "스파르타의 전사들이 사용했다는 전설의 창입니다.");
            IItem armor1 = new Armor("수련자의 갑옷", 5, 1000, "쉽게 볼 수 있는 낡은 검 입니다.");
            IItem armor2 = new Armor("무쇠 갑옷", 9, 1500, "쉽게 볼 수 있는 낡은 검 입니다.");
            IItem armor3 = new Armor("스파르타의 갑옷", 15, 3500, "쉽게 볼 수 있는 낡은 검 입니다.");

            IItem[] items = {weapon1, weapon2, weapon3, armor1, armor2, armor3};


            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");

            StartScene();









            void StartScene()
            {
                int insert;

                Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
                Console.WriteLine("1. 상태보기\n2. 인벤토리\n3. 상점\n");
                Console.Write("원하시는 행동을 입력해주세요\n>>");
                while (true)
                {
                    bool insertbool = int.TryParse(Console.ReadLine(), out insert);
                    Console.WriteLine("");
                    if (insertbool)
                    {
                        switch (insert)
                        {
                            case 1:
                                Status();
                                break;
                            case 2:
                                Inventory();
                                break;
                            case 3:
                                Shop();
                                break;
                            default:
                                Console.WriteLine("잘못된 입력입니다.");
                                break;
                        }
                    }
                    else
                        Console.WriteLine("잘못된 입력입니다.");
                }
            }

            void Status()
            {
                int insert;
                string lvlText = (player.Level).ToString("D2");
                int itemAttack = 0;
                int itemDefence = 0;

                for(int i = 0; i<items.Length; i++)
                {
                    if(i < 3)
                    {
                        if (items[i].Purchased)
                        {
                            if (items[i].Equipted)
                                itemAttack += items[i].Figure;
                        }
                    }
                    else
                    {
                        if (items[i].Purchased)
                        {
                            if (items[i].Equipted)
                                itemDefence += items[i].Figure;
                        }
                    }
                }


                Console.WriteLine("Lv. " + lvlText);
                Console.WriteLine($"{player.Name} ( {player.Job} )");
                Console.Write("공격력 : " + player.Attack);
                if(itemAttack > 0)
                {
                    Console.Write($"(+{itemAttack})");
                }
                Console.Write("\n방어력 : " + player.Defence);
                if (itemDefence > 0)
                {
                    Console.Write($"(+{itemDefence})");
                }

                Console.WriteLine("\n체 력 : " + player.Health);
                Console.WriteLine("Gold : " + player.Gold + " G");

                Console.WriteLine("\n0. 나가기\n");

                Console.Write("원하시는 행동을 입력해주세요\n>>");

                while (true)
                {
                    bool insertbool = int.TryParse(Console.ReadLine(), out insert);
                    Console.WriteLine("");
                    if (insertbool)
                    {
                        switch (insert)
                        {
                            case 0:
                                StartScene();
                                break;
                            default:
                                Console.WriteLine("잘못된 입력입니다.");
                                break;
                        }
                    }
                    else
                        Console.WriteLine("잘못된 입력입니다.");
                }

            }

            void Inventory()
            {
                int insert;

                Console.WriteLine("[아이템 목록]\n");
                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i].Purchased)
                    {
                        if(i < 3)
                        {
                            Console.Write($" - {items[i].ItemName}\t|");
                            Console.Write($"공격력 +{items[i].Figure}\t|");
                            Console.Write(items[i].Description + "\n");
                        }
                        else
                        {
                            Console.Write($" - {items[i].ItemName}\t|");
                            Console.Write($"방어력 +{items[i].Figure}\t|");
                            Console.Write(items[i].Description + "\n");
                        }
                    }
                }

                Console.WriteLine("\n1. 장착 관리");
                Console.WriteLine("0. 나가기\n");


                Console.Write("원하시는 행동을 입력해주세요\n>>");

                while (true)
                {
                    bool insertbool = int.TryParse(Console.ReadLine(), out insert);
                    Console.WriteLine("");
                    if (insertbool)
                    {
                        switch (insert)
                        {
                            case 0:
                                StartScene();
                                break;
                            case 1:
                                ItemBox();
                                break;
                            default:
                                Console.WriteLine("잘못된 입력입니다.");
                                break;
                        }
                    }
                    else
                        Console.WriteLine("잘못된 입력입니다.");
                }
            }

            void Shop()
            {
                int insert;

                Console.WriteLine("[보유 골드]");
                Console.WriteLine(player.Gold + "G\n");

                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < items.Length; i++)
                {
                    Console.Write($" - {items[i].ItemName}\t|");
                    Console.Write($"공격력 +{items[i].Figure}\t|");
                    Console.Write(items[i].Description + "\t|");
                    if (!items[i].Purchased)
                        Console.WriteLine(items[i].Price + "G");
                    else
                        Console.WriteLine("구매완료");

                }

                Console.WriteLine("\n1. 아이템 구매");
                Console.WriteLine("0. 나가기\n");


                Console.Write("원하시는 행동을 입력해주세요\n>>");

                while (true)
                {
                    bool insertbool = int.TryParse(Console.ReadLine(), out insert);
                    Console.WriteLine("");
                    if (insertbool)
                    {
                        switch (insert)
                        {
                            case 0:
                                StartScene();
                                break;
                            case 1:
                                Shopping();
                                break;
                            default:
                                Console.WriteLine("잘못된 입력입니다.");
                                break;
                        }
                    }
                    else
                        Console.WriteLine("잘못된 입력입니다.");
                }
            }

            void ItemBox()
            {
                while (true)
                {
                    int insert;
                    int n = 1;
                    List<IItem> itemsPurchased = new List<IItem>();

                    Console.WriteLine("[아이템 목록]\n");
                    for (int i = 0; i < items.Length; i++)
                    {
                        if (items[i].Purchased)
                        {
                            if (i < 3)
                            {
                                Console.Write($" - {n} {items[i].ItemName}\t|");
                                Console.Write($"공격력 +{items[i].Figure}\t|");
                                Console.Write(items[i].Description + "\n");
                                n++;
                                itemsPurchased.Add(items[i]);
                            }
                            else
                            {
                                Console.Write($" - {n} {items[i].ItemName}\t|");
                                Console.Write($"방어력 +{items[i].Figure}\t|");
                                Console.Write(items[i].Description + "\n");
                                n++;
                                itemsPurchased.Add(items[i]);
                            }
                        }
                    }

                    Console.WriteLine("\n0. 나가기\n");


                    Console.Write("원하시는 행동을 입력해주세요\n>>");

                    while (true)
                    {
                        bool insertbool = int.TryParse(Console.ReadLine(), out insert);
                        Console.WriteLine("");
                        if (insertbool)
                        {
                            if (insert <= itemsPurchased.Count && insert > 0)
                            {
                                if (!itemsPurchased[insert - 1].Equipted)
                                {
                                    itemsPurchased[insert - 1].Wear(player);
                                    break;
                                }
                                else
                                {
                                    itemsPurchased[insert - 1].TakeOff(player);
                                    break;
                                }
                            }
                            else if (insert == 0)
                            {
                                StartScene();
                            }
                        }
                        else
                            Console.WriteLine("잘못된 입력입니다.");
                    }
                }
            }

            void Shopping()
            {
                int insert;

                while (true)
                {
                    int n = 1;


                    Console.WriteLine("[보유 골드]");
                    Console.WriteLine(player.Gold + "G\n");

                    Console.WriteLine("[아이템 목록]");
                    for (int i = 0; i < items.Length; i++)
                    {
                        Console.Write($" - {n} {items[i].ItemName}\t|");
                        Console.Write($"공격력 +{items[i].Figure}\t|");
                        Console.Write(items[i].Description + "\t|");
                        if (!items[i].Purchased)
                            Console.WriteLine(items[i].Price + "G");
                        else
                            Console.WriteLine("구매완료");
                        n++;

                    }


                    Console.WriteLine("\n0. 나가기\n");


                    Console.Write("원하시는 행동을 입력해주세요\n>>");

                    while (true)
                    {
                        bool insertbool = int.TryParse(Console.ReadLine(), out insert);
                        Console.WriteLine("");
                        if (insertbool)
                        {
                            if(insert == 0)
                            {
                                StartScene();
                            }
                            else if(insert > 0 && insert <= items.Length)
                            {
                                if (items[insert - 1].Purchased)
                                {
                                    Console.WriteLine("이미 구매한 상품입니다.");
                                }
                                else
                                {
                                    if (items[insert - 1].Price > player.Gold)
                                    {
                                        Console.WriteLine("골드가 부족합니다");
                                        break;
                                    }
                                    else
                                    {
                                        items[insert - 1].Purchase(player);
                                        break;
                                    }
                                }
                            }
                        }
                        else
                            Console.WriteLine("잘못된 입력입니다.");
                    }
                }

            }
        }
    }
}
