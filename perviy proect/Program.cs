using System;
using System.ComponentModel.Design;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Xml.Schema;

namespace perviy_proect
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string word = GenerateRandomWord();
			
			int cnt = 0;
			int letterCounter = 0;
			int totalMoney = 0;
			bool guessed = false;
			int sumBall = 0;

			string hiddenWord = HideWord(word);
			Console.WriteLine(hiddenWord);

			string sector = GenerateRandomSector();

			while (hiddenWord != word)
			{
				string tempHiddenWord = "";
				//
				//ЕСЛИ СУЕТА НА БАРАБАНЕ----------------------------------
				if (sector == "+")
				{
					while (guessed == false)
					{
						Console.WriteLine("Выберите букву!");
						string letterSpawn = Console.ReadLine();
						int numberr;
						if (int.TryParse(letterSpawn, out numberr))
						{
							int freeLetter = int.Parse(letterSpawn);
							for (int g = 0; g < hiddenWord.Length; g++)
							{
								if (g == freeLetter - 1)
									tempHiddenWord += word[freeLetter - 1];
								else
									tempHiddenWord += hiddenWord[g];
							}
							guessed = true;
						}
						else
							Console.WriteLine("Такая буква уже открыта или её нет в слове");
					}
				}
				if (sector == "Б")
				{
					Console.WriteLine("Баллы на барабане: " + sector + " Обанкротился родной");
					sumBall = 0;
					break;
				}
				else if (sector == "П")
				{
					Console.WriteLine("Баллы на барабане: " + sector + " Буква П на барабане!!!");
					Console.WriteLine("Вы хотите забрать приз или деньги?");
					string otvet = Console.ReadLine();
					if (otvet == "Приз")
					{
						Console.WriteLine("Поздравляем! Вы выиграли Ферреро-Роше!");
					}
					else if (otvet == "Деньги")
					{
						Console.WriteLine("Поздравляем! Вы выиграли деньги");
					}
					break;
				}

				Console.WriteLine();
				Console.WriteLine("Баллы на барабане: " + sector + " Сумма баллов:" + sumBall);

				char userletter = Console.ReadLine()[0];
				cnt++;
				//
				//
				//УГАДЫВАЕМ СЛОВО------------------------------------------------------
				for (int i = 0; i < word.Length; i++)
				{
					if (word[i] == userletter)
					{
						tempHiddenWord = tempHiddenWord + userletter;
						letterCounter++;
						if (sector == "Х2")
						{
							Console.WriteLine("Вам выпало X2 Баллов");
							sumBall = sumBall * 2;
						}
					}
					else
					{
						tempHiddenWord = tempHiddenWord + hiddenWord[i-1];
						letterCounter = 0;
					}

					if (tempHiddenWord != hiddenWord)
					{
						int intball = int.Parse(sector);
						sumBall = intball + sumBall;
					}
					hiddenWord = tempHiddenWord;
					Console.WriteLine(hiddenWord + " " + cnt);
					//
					//
					//ШКАТУЛКА
					if (letterCounter > 2)
					{
						totalMoney = totalMoney + GuessBoxex();
						letterCounter = 0;
					}
				}
				if (hiddenWord != word)
					Console.WriteLine("Ура! Вы победили!!!");
				else
					Console.WriteLine("К сожалению,Вы проиграли ;[");
				//
				Console.WriteLine();
				Console.WriteLine("Твоя сумма баллов:" + sumBall);
				//
				//
				//ПРИЗЫ-----------------------------------------------------------
				while (sumBall != 0)
				{
					string[] store = ["пылесос", "фото с якубовичем", "билет на эль-классико", "автомобиль", "Ферреро-Роше"];
					int[] storePrice = [1000, 2000, 2500, 3500, 5000];
					if (sumBall == 0)
					{
						Console.WriteLine("давай джабах у бичо");
					}
					Console.WriteLine($"Выберите номер приза: {store} и цена призов: {storePrice}");
					int choisePrize = Convert.ToInt32(Console.ReadLine());
					int choosenPrize = 0;
					if (sumBall == storePrice[choisePrize - 1])
					{
						if (choisePrize != choosenPrize)
						{
							Console.WriteLine("Забирайте приз!");
							sumBall = sumBall - storePrice[choisePrize - 1];
							choosenPrize = choisePrize;
						}
						else Console.WriteLine("Приз уже забрали!");

					}
					else { Console.WriteLine("У вас не хватает баллов"); }
				}
			}
		}
		static string GenerateRandomWord()
		{
			string[] wordsBank = ["дерево", "дурь"];
			int randomWordIndex = new Random().Next(0, wordsBank.Length);
			string word = wordsBank[randomWordIndex];
			return word;
		}
		static string GenerateRandomSector()
		{
			string[] sectors = ["700", "600", "650", "0", "Б", "П", "Х2", "+", "350", "450", "550", "500", "750"];

			int scoreIndex = new Random().Next(0, sectors.Length);
			string sector = sectors[scoreIndex];
			return sector;
		}
		static string HideWord(string word)
		{
			string hiddenWord = "";
			for (int i = 0; i < word.Length; i++)
			{
				hiddenWord = hiddenWord + "*";
			}

			return hiddenWord;
		}
		static int GuessBoxex()
		{
			Console.WriteLine("Вы можете выбрать одну из трех шкатулок, в которой есть приз!");
			int numBox = Convert.ToInt32(Console.ReadLine());
			int indexBox = new Random().Next(1, 4);

			if (numBox == indexBox)
			{
				Console.WriteLine($"Ты выбрал шкатулку под номером {numBox}! Ты выигрываешь 5000 рублей");
				return 5000;
			}
			else
			{
				Console.WriteLine($"Вы не угадали! Приз был в {indexBox} шкатулке");
				return 0;
			}
		}
	}
}