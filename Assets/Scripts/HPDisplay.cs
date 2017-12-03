using UnityEngine;
using UnityEngine.UI;

public class HPDisplay : MonoBehaviour
{
	private Image[] _hpImages;

	public Sprite _full;
	public Sprite _depleted;
	public Sprite _corrupted;

	private void Awake()
	{
		_hpImages = GetComponentsInChildren<Image>();
	}

	public void Set(int max, int remaining, int corrupted)
	{
		//char[] hpbar = new char[_hpImages.Length];
		for (int i = 0; i < remaining; i++)
		{
			if (_hpImages[i].sprite != _full)
			{
				_hpImages[i].sprite = _full;
			}
		}
		for (int i = remaining; i < max - corrupted; i++)
		{
			if (_hpImages[i].sprite != _depleted)
			{
				_hpImages[i].sprite = _depleted;
			}
		}
		for (int i = max - corrupted; i < max; i++)
		{
			if (_hpImages[i].sprite != _corrupted)
			{
				_hpImages[i].sprite = _corrupted;
			}
		}

		for (int i = max; i < _hpImages.Length; i++)
		{
			if (_hpImages[i].sprite != null)
			{
				_hpImages[i].sprite = null;
			}
		}
	}
}