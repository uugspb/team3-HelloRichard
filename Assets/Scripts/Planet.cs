using UnityEngine;
using System.Collections;
using DG.Tweening;


public class Planet : MonoBehaviour {

	bool isFocused = false;

	public enum AnimationClipType { POSITION, ROTATION, SCALE };
	Vector3 originalScale;
	public Animation animation;

	float scaleValue = 1.5f;
	float scaleTime = 0.5f;

	public bool IsFocused
	{
		get
		{
			return isFocused;
		}
	}

	public void Focus()
	{
		isFocused = true;

		if (animation.GetClip ("ScaleUp"))
			animation.RemoveClip ("ScaleUp");

		if (animation.isPlaying)
			animation.Stop ();

		float c = scaleTime / ((originalScale.x * scaleValue) - originalScale.x);
		float time = ((originalScale.x * scaleValue) - transform.localScale.x) * c;

		AnimationClip clip = CreateAnimationClip (AnimationClipType.SCALE, transform.localScale, originalScale * scaleValue, time);

		animation.AddClip (clip, "ScaleUp");
		animation.Play ("ScaleUp");


	}

	public void Unfocus()
	{
		//transform.DOMove (new Vector3 (0, 0, 0), 1);
		isFocused = false;


		if (animation.GetClip ("ScaleDown"))
			animation.RemoveClip ("ScaleDown");

		if (animation.isPlaying)
			animation.Stop ();

		float c = scaleTime / ((originalScale.x * scaleValue) - originalScale.x);
		float time = ((originalScale.x * scaleValue) - transform.localScale.x) * c;

		AnimationClip clip = CreateAnimationClip (AnimationClipType.SCALE, transform.localScale, originalScale, scaleTime - time);

		animation.AddClip (clip, "ScaleDown");
		animation.Play ("ScaleDown");
	}

	static string GetCoordinate(int index)
	{
		switch(index)
		{
		case 0:
			return "x"; break;
		case 1:
			return "y"; break;
		case 2:
			return "z"; break;
		default:
			return "w"; break;
		}
	}

	static public AnimationClip CreateAnimationClip(AnimationClipType type, Vector3 begin, Vector3 end, float time, float beginTime = 0f, AnimationClip clip = null)
	{
		if(clip == null)
			clip = new AnimationClip();
		clip.legacy = true;

		switch(type)
		{
		case AnimationClipType.POSITION:
			{
				for(int i=0; i<3; ++i)
					clip.SetCurve("", typeof(Transform), "localPosition." + GetCoordinate(i), new AnimationCurve(new Keyframe(0, begin[i]), new Keyframe(beginTime, begin[i]), new Keyframe(beginTime + time, end[i])));
			} break;

		case AnimationClipType.SCALE:
			{
				for(int i=0; i<3; ++i)
					clip.SetCurve("", typeof(Transform), "localScale." + GetCoordinate(i), new AnimationCurve(new Keyframe(0, begin[i]), new Keyframe(beginTime, begin[i]), new Keyframe(beginTime + time, end[i])));
			} break;

			/*case AnimationClipType.ROTATION:
		{
			for(int i=0; i<4; ++i)
				clip.SetCurve("", typeof(Transform), "localRotation." + GetCoordinate(i), new AnimationCurve(new Keyframe(0, begin[i]), new Keyframe(beginTime, begin[i]), new Keyframe(beginTime + time, end[i])));

		}*/

		default: break;
		}

		return clip;
	}

	void Start () 
	{
		originalScale = transform.localScale;
		animation = GetComponent<Animation> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
