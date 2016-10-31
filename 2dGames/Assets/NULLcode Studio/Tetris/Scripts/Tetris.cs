// NULLcode Studio © 2016
// null-code.ru

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tetris : MonoBehaviour {

	public GameObject mainCube; // куб из которого будут создаваться фигуры, его размер должен соответствовать - 1 (как у стандартного куба Юнити)
	public float speed = 1; // скорость падания
	public Color[] color; // цвета для фигур, их должно быть столько же, сколько самих фигур

	public KeyCode _right = KeyCode.A; // клавиши управления
	public KeyCode _left = KeyCode.D;
	public KeyCode _rotateLeft = KeyCode.E;
	public KeyCode _rotateRight = KeyCode.Q;
	public KeyCode _down = KeyCode.S;

	private int width = 12; // ширина игрового поля
	private int height = 20; // высота игрового поля

	private GameObject[,] field = new GameObject[0, 0];
	private string[] shapeName = {"O", "L", "J", "T", "S", "Z", "I"}; // условное обозначение фигур, чтобы проще было ориентироваться
	private GameObject sample, current;
	private int X_field;
	private float timeout, curSpeed;
	private List<GameObject> shape = new List<GameObject>();

	void Start()
	{
		sample = new GameObject(); // вспомогательный объект, для разворота фигур
		X_field = width/2;
		NewGame();
	}

	void NewGame() // старт новой игры, очистка игрового поля
	{
		for(int i = 0; i < transform.childCount; i++)
		{
			Destroy(transform.GetChild(i).gameObject);
		}
		foreach(GameObject obj in shape)
		{
			Destroy(obj);
		}

		field = new GameObject[width, height];
		CreateShape();

		Debug.Log("Старт новой игры!");
	}

	void CreateCube(Color cubeColor) // создание куба, покраска, добавление в массив фигуры
	{
		current = Instantiate(mainCube) as GameObject;
		current.GetComponent<MeshRenderer>().material.color = cubeColor;
		shape.Add(current);
	}
		
	void CreateShape() // создание массива фигуры
	{
		sample.transform.localEulerAngles = Vector3.zero; // сброс вращения для вспомогательного объекта
		shape = new List<GameObject>(); // очистка массива фигуры
		int j = Random.Range(0, shapeName.Length); // выбор случайной фигуры

		switch(shapeName[j])
		{
		case "I":
			for(int i = 0; i < 4; i++) // цикл фигуры, расстановка кубов в форму (тут будет линия)
			{
				CreateCube(color[0]);
				switch(i)
				{
				case 0:
					current.transform.position = new Vector2(X_field, -3);
					break;
				case 1:
					current.transform.position = new Vector2(X_field, -2);
					break;
				case 2:
					current.transform.position = new Vector2(X_field, -1);
					break;
				case 3:
					current.transform.position = new Vector2(X_field, 0);
					break;
				}
			}
			break;

		case "O": // просто один куб, как бы квадрат
			CreateCube(color[1]);
			current.transform.position = new Vector2(X_field, 0);
			break;

		case "L":
			for(int i = 0; i < 4; i++)
			{
				CreateCube(color[2]);
				switch(i)
				{
				case 0:
					current.transform.position = new Vector2(X_field, 0);
					break;
				case 1:
					current.transform.position = new Vector2(X_field, -1);
					break;
				case 2:
					current.transform.position = new Vector2(X_field, -2);
					break;
				case 3:
					current.transform.position = new Vector2(X_field + 1, -2);
					break;
				}
			}
			break;

		case "J":
			for(int i = 0; i < 4; i++)
			{
				CreateCube(color[3]);
				switch(i)
				{
				case 0:
					current.transform.position = new Vector2(X_field, 0);
					break;
				case 1:
					current.transform.position = new Vector2(X_field, -1);
					break;
				case 2:
					current.transform.position = new Vector2(X_field,  -2);
					break;
				case 3:
					current.transform.position = new Vector2(X_field - 1,  -2);
					break;
				}
			}
			break;

		case "S":
			for(int i = 0; i < 4; i++)
			{
				CreateCube(color[4]);
				switch(i)
				{
				case 0:
					current.transform.position = new Vector2(X_field, 0);
					break;
				case 1:
					current.transform.position = new Vector2(X_field + 1, 0);
					break;
				case 2:
					current.transform.position = new Vector2(X_field, -1);
					break;
				case 3:
					current.transform.position = new Vector2(X_field - 1, -1);
					break;
				}
			}
			break;

		case "Z":
			for(int i = 0; i < 4; i++)
			{
				CreateCube(color[5]);
				switch(i)
				{
				case 0:
					current.transform.position = new Vector2(X_field, 0);
					break;
				case 1:
					current.transform.position = new Vector2(X_field - 1, 0);
					break;
				case 2:
					current.transform.position = new Vector2(X_field, -1);
					break;
				case 3:
					current.transform.position = new Vector2(X_field + 1, -1);
					break;
				}
			}
			break;

		case "T":
			for(int i = 0; i < 4; i++)
			{
				CreateCube(color[6]);
				switch(i)
				{
				case 0:
					current.transform.position = new Vector2(X_field, 0);
					break;
				case 1:
					current.transform.position = new Vector2(X_field, -1);
					break;
				case 2:
					current.transform.position = new Vector2(X_field - 1, 0);
					break;
				case 3:
					current.transform.position = new Vector2(X_field + 1, 0);
					break;
				}
			}
			break;
		}

		if(GameOver()) // если после создание фигуры движение вниз невозможно - старт новой игры
		{
			Debug.Log("Конец игры...");	
			NewGame();
		}
	}

	bool GameOver() // проверка на Game Over
	{
		for(int i = 0; i < shape.Count; i++)
		{
			Transform tr = shape[i].transform;
			int x = Mathf.RoundToInt(tr.position.x);
			int y = Mathf.Abs(Mathf.RoundToInt(tr.position.y));

			if(y < height - 1) 
			{
				if(field[x, y + 1]) 
				{
					return true; // если внизу клетка занята
				}
			}
		}

		return false;
	}

	void AddToField() // добавление фигуры в 2D массив
	{
		for(int i = 0; i < shape.Count; i++)
		{
			Transform tr = shape[i].transform;
			int x = Mathf.RoundToInt(tr.position.x); // целое число, позиция по Х
			int y = Mathf.Abs(Mathf.RoundToInt(tr.position.y)); // целое число, позиция по Y, так как у нас по этой оси отрицательные значение, возвращаем абсолютное
			field[x, y] = tr.gameObject;
			tr.parent = transform;
		}
	}

	bool Down() // движение вниз
	{
		for(int i = 0; i < shape.Count; i++)
		{
			Transform tr = shape[i].transform;
			int x = Mathf.RoundToInt(tr.position.x);
			int y = Mathf.Abs(Mathf.RoundToInt(tr.position.y));

			if(y < height - 1) 
			{
				if(field[x, y + 1]) 
				{
					return false; // если внизу клетка занята
				}
			}
			else
			{
				return false; // если ниже конец массива
			}
		}

		// если всё нормально, сдвигаем фигуру вниз
		foreach(GameObject obj in shape)
		{
			obj.transform.position -= new Vector3(0, 1, 0);	
		}

		return true;
	}

	void Move(int index) // сдвиг фигуры влево или вправо
	{
		foreach(GameObject obj in shape)
		{
			obj.transform.position += new Vector3(index, 0, 0);
		}
	}

	bool RightLeft(int index) // можно-ли сдвинуть влево или вправо, проверка
	{
		for(int i = 0; i < shape.Count; i++)
		{
			Transform tr = shape[i].transform;
			int x = Mathf.RoundToInt(tr.position.x);
			int y = Mathf.Abs(Mathf.RoundToInt(tr.position.y));

			if(x < width - 1 && index > 0) 
			{
				if(field[x + 1, y]) 
				{
					return false;
				}
			}
			else if(x > 0 && index < 0) 
			{
				if(field[x - 1, y]) 
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}

		return true;
	}

	bool InsideField() // проверка, фигура внутри поля или нет
	{
		for(int i = 0; i < shape.Count; i++)
		{
			Transform tr = shape[i].transform;
			int x = Mathf.RoundToInt(tr.position.x);
			int y = Mathf.Abs(Mathf.RoundToInt(tr.position.y));

			if(x < 0 || x > width - 1 || y > height - 1 || y < 0)
			{
				return false;
			}
		}

		return true;
	}

	bool CheckOverlap() // проверка на перекрытие с другими фигурами
	{
		for(int i = 0; i < shape.Count; i++)
		{
			Transform tr = shape[i].transform;
			int x = Mathf.RoundToInt(tr.position.x);
			int y = Mathf.Abs(Mathf.RoundToInt(tr.position.y));

			if(field[x, y])
			{
				return true;
			}
		}

		return false;
	}

	bool ShiftShape(int iter, int shift) // функция сдвига фигуры, если при развороте она выходит за рамки поля
	{
		Move(shift); // сдвигаем вправо
		if(InsideField()) // фигура попала в поле?
		{
			if(CheckOverlap()) // фигура перекрывает другие?
			{
				Move(-shift); // возврат позиции
				return true;
			}
			return false;
		}

		Move(-shift * 2); // тоже самое в обратную сторону
		if(InsideField())
		{
			if(CheckOverlap())
			{
				Move(shift);
				return true;
			}
			return false;
		}
		Move(shift);

		if(iter > 0) // если итерация позволяет
		{
			return ShiftShape(iter - 1, shift + 1); // повтор функции и добавление шага сдвига
		} 
		else 
		{
			return true;
		}
	}

	void Rotation(int index) // функция поворота фигуры
	{
		bool result = false;
		sample.transform.position = shape[0].transform.position; // установка центра в позицию первого куба фигуры

		// делаем фигуру дочерней шаблону, затем добавляем вращение, после - убираем дочерние объекты из шаблона
		foreach(GameObject obj in shape)
		{
			obj.transform.parent = sample.transform;
		}
		sample.transform.Rotate(0, 0, index);
		foreach(GameObject obj in shape)
		{
			obj.transform.parent = null;
		}

		// если фигура за рамками поля, сдвигаем ее
		// если она внутри поля, делаем проверку на перекрытие
		if(!InsideField())
		{
			result = ShiftShape(2, 1);
		}
		else 
		{
			result = CheckOverlap();
		}

		// если одна из функций вернет true, возврат в прежнее положение
		if(result) 
		{
			foreach(GameObject obj in shape)
			{
				obj.transform.parent = sample.transform;
			}
			sample.transform.Rotate(0, 0, -index);
			foreach(GameObject obj in shape)
			{
				obj.transform.parent = null;
			}
		}
	}

	void FieldUpdate() // обновление игрового поля
	{
		shape = new List<GameObject>();
		for(int i = 0; i < transform.childCount; i++) 
		{
			shape.Add(transform.GetChild(i).gameObject); // добавляем все кубы в массив
		}

		int line = -1;
		line = CheckLine();
		while(line != -1)
		{
			DestroyLine(line);
			ShiftLine(line);
			line = CheckLine();
		}
	}

	void ShiftLine(int line) // сдвиг кубов
	{
		// смещение элементов от указанной линии (снизу-вверх)
		for(int x = 0; x < width; x++) 
		{
			for(int y = line; y > 0; y--)
			{
				field[x, y] = field[x, y - 1];
			}
		}

		// oбнуление самого верхнего ряда
		for (int i = 0; i < width; i++) 
		{
			field[i, 0] = null;
		}

		shape = new List<GameObject>();
		for(int i = 0; i < transform.childCount; i++) 
		{
			shape.Add(transform.GetChild(i).gameObject); // добавляем все кубы в массив
		}

		// сдвигаем кубики вниз, от указанной линии
		foreach(GameObject obj in shape)
		{
			int y = Mathf.RoundToInt(Mathf.Abs(obj.transform.position.y));
			if(y < line) 
			{
				obj.transform.position -= new Vector3(0, 1, 0);
			}
		}
	}

	void DestroyLine(int line) // удаление полной линии
	{
		foreach(GameObject obj in shape)
		{
			int x = Mathf.RoundToInt(obj.transform.position.x);
			int y = Mathf.RoundToInt(Mathf.Abs(obj.transform.position.y));
			if(y == line)
			{
				field[x, y] = null;
				Destroy(obj);
			}
		}
	}

	int CheckLine() // поиск полной линии
	{
		int i = 0;
		for(int y = 0; y < height; y++)
		{
			for(int x = 0; x < width; x++)
			{
				if (field[x, y])
					i++;
			}
			if(i == width)
			{
				return y;
			}
			i = 0;
		}
		return -1;
	}
		
	void Update()
	{
		if(Input.GetKeyDown(_right) && RightLeft(-1))
		{
			Move(-1);
		}
		else if(Input.GetKeyDown(_left) && RightLeft(1))
		{
			Move(1);
		}
		else if(Input.GetKeyDown(_rotateLeft))
		{
			Rotation(-90);
		}
		else if(Input.GetKeyDown(_rotateRight))
		{
			Rotation(90);
		}
		else if(Input.GetKey(_down))
		{
			curSpeed = 0;
		}
		else
		{
			curSpeed = speed;
		}

		curSpeed = Mathf.Clamp(curSpeed, 0.1f, 1f); // ограничение по макс/мин скорости
			
		timeout += Time.deltaTime;
		if(timeout > curSpeed)
		{
			timeout = 0;
			if(!Down())
			{
				AddToField();
				FieldUpdate();
				CreateShape();
			} 
		}
	}
}
