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
			string[] wordsBank = ["дерево", "дурь"];
			int randomWordIndex = new Random().Next(0, wordsBank.Length);

			string word = wordsBank[randomWordIndex];
			string HiddenWord = "";
			int cnt = 0;
			int letter_counter = 0;
			int totalmoney = 0;
			bool guessed = false;
			int sumball = 0;

			for (int i = 0; i < word.Length; i++)
			{
				HiddenWord = HiddenWord + "*";
			}

			Console.WriteLine(HiddenWord);

			List<int> possible_balls = new List<int>() { 10, 0, 100, 200, 500, 777, 1000 };
			string[] sektora = ["700", "600", "650", "0", "Б", "П", "Х2", "+", "350", "450", "550", "500", "750"];

			int ballindex = new Random().Next(0, sektora.Length);
			string ball = sektora[ballindex];

			while (HiddenWord != word)
			{
				}
				Console.WriteLine();
				Console.WriteLine("Баллы на барабане: " + ball + " Сумма баллов:" + sumball);

				char userletter = Console.ReadLine()[0];
				cnt++;

				string tempHiddenWord = "";

				for (int i = 0; i < word.Length; i++)
				{
				//ЕСЛИ КАКАЯ ТО СУЕТА НА БАРАБАНЕ----------------------------------------------------------------------------------
				if (ball == "+")
				{
					while (guessed == false)
					{
						Console.WriteLine("Выберите букву!");
						string letter_plus = Console.ReadLine();
						int numberr;
						if (int.TryParse(letter_plus, out numberr))
						{
							int letter_plus1 = int.Parse(letter_plus);
							for (int g = 0; i < HiddenWord.Length; i++)
							{
								if (g == letter_plus1 - 1)
									tempHiddenWord += word[letter_plus1 - 1];
								else
									tempHiddenWord += HiddenWord[g];
							}
							guessed = true;
						}
						else
							Console.WriteLine("Такая буква уже открыта или её нет в слове");
					}
				}

				if (ball == "Б")
				{
					Console.WriteLine("Баллы на барабане: " + ball + " Обанкротился родной");
					sumball = 0;
					break;
				}
				else if (ball == "П")
				{
					Console.WriteLine("Баллы на барабане: " + ball + " Буква П на барабане!!!");
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

				else if (ball == "Х2")
				{
					Console.WriteLine("Вам выпало X2 Баллов");
					sumball = sumball * 2;
				}	


				if (letter_counter > 2)
				{
					letter_counter = 0;
					Console.WriteLine("Вы можете выбрать одну из трех шкатулок, в которой есть приз!");
					int num_chkatul = Convert.ToInt32(Console.ReadLine());
					int indexchkatulky = new Random().Next(1, 4);
					
					if (num_chkatul == indexchkatulky)
					{
						Console.WriteLine($"Ты выбрал шкатулку под номером {num_chkatul}! Ты выигрываешь 5000 рублей");
						totalmoney = totalmoney + 5000;
					}
					else
					{
						Console.WriteLine($"Вы не угадали! Приз был в {indexchkatulky} шкатулке");
					}
				}
				//
				//УГАДЫВАЕМ СЛОВО------------------------------------------------------
				if (word[i] == userletter)
				{
					tempHiddenWord = tempHiddenWord + userletter;
					letter_counter++;
				}
				else
				{
					tempHiddenWord = tempHiddenWord + HiddenWord[i];
					letter_counter = 0;
				}

				if (tempHiddenWord != HiddenWord)
				{
					int intball = int.Parse(ball);
					sumball = intball + sumball;
				}
				HiddenWord = tempHiddenWord;
				Console.WriteLine(HiddenWord + " " + cnt);
			}
			if (HiddenWord != word)
				Console.WriteLine("Ура! Вы победили!!!");
			else
				Console.WriteLine("К сожалению,Вы проиграли ;[");
			Console.WriteLine();
			Console.WriteLine("Твоя сумма баллов:" + sumball);
			//
			//
			//ПРИЗЫ-----------------------------------------------------------
			while (sumball != 0)
			{
				string[] store_ballov = ["пылесос", "фото с якубовичем", "билет на эль-классико", "автомобиль", "Ферреро-Роше"];
				int[] store_ballov_price = [1000, 2000, 2500, 3500, 5000];
				if (sumball == 0)
				{
					Console.WriteLine("давай джабах у бичо");
				}
				Console.WriteLine($"Выберите номер приза: {store_ballov} и цена призов: {store_ballov_price}");
				int choise_prize = Convert.ToInt32(Console.ReadLine());
				int choosen_prize = 0;
				if (sumball == store_ballov_price[choise_prize - 1])
				{
					if (choise_prize != choosen_prize)
					{
						Console.WriteLine("Забирайте приз!");
						sumball = sumball - store_ballov_price[choise_prize - 1];
						choosen_prize = choise_prize;
					}
					else Console.WriteLine("Приз уже забрали!");

				}
				else { Console.WriteLine("У вас не хватает баллов"); }
			}
		}
	}
}