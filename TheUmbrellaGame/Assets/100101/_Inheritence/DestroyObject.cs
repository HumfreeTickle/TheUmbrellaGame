using UnityEngine;
using System.Collections;

namespace Inheritence
{
	public class DestroyObject
	{
		private float _timer;
//-------------------------------------- Destroys GameObject On Collision -------------------------------------------------------
		public void DestroyOnCollsion (Collision Col)
		{
			if (Col.gameObject.tag == "Collectable") {
				Col.gameObject.SetActive (false);
			}
		}//end

//-------------------------------------- Destroys GameObject On Timer Countdown -------------------------------------------------------

		public void DestroyOnTimer (GameObject objectToDestroy , float DestroyDelay)
		{
			_timer += Time.deltaTime;

			if (_timer > DestroyDelay) {
				MonoBehaviour.Destroy (objectToDestroy);
			}//end

		}//end

//-------------------------------------- Destroys GameObject On Button Press -------------------------------------------------------

		public void DestroyOnButton ()
		{

		}//end

//-------------------------------------- Destroys GameObject On Trigger -------------------------------------------------------


		public void DestroyOnTrigger ()
		{

		}//end
	}//end
}//end
