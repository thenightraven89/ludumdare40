using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSManager : MonoBehaviour
{
	public static PSManager Instance { get; private set; }

	public ParticleSystem _bloodPS;
	
	private void Awake()
	{
		Instance = this;
	}

	public void EmitBlood(int count, Vector3 position)
	{
		//var emitParams = new ParticleSystem.EmitParams();
		//emitParams.pos = position;
		_bloodPS.transform.position = position;
		_bloodPS.Emit(count);
	}
}