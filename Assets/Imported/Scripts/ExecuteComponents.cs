using UnityEngine;
using UnityEngine.UI;

public class ExecuteComponents : MonoBehaviour
{
	[SerializeField] private Transform component0; // using primetween to move in Z from 5.9f to 0f
	[SerializeField] private Transform component1; // using primetween to rotate in Z from 0f to 90f
	[SerializeField] private Transform component2; // using primetween to move in Z from 0f to -23f
	[SerializeField] private Transform component3; // using primetween to move in Z from 0f to -23f
	[SerializeField] private Transform component4; // using primetween to scale in Z from 1f to 0.35f

	[SerializeField] private float duration = 1.5f;
	[SerializeField] private Button animateBtn;
	private bool btnStateFlag = true; // to prevent multiple clicks on button while animation is running
	private bool toggleFlag;

	// Summary ::
	// after component0 animation is completed,
	// execute animation on component1, component2, component3 and component4 in parallel
	private void Start() => HardReset();
	public void HardReset()
	{
		component0.localPosition = new Vector3(component0.localPosition.x, component0.localPosition.y, 5.9f);
		component1.localEulerAngles = new Vector3(component1.localEulerAngles.x, component1.localEulerAngles.y, 0f);
		component2.localPosition = new Vector3(component2.localPosition.x, component2.localPosition.y, 0f);
		component3.localPosition = new Vector3(component3.localPosition.x, component3.localPosition.y, 0f);
		component4.localScale = new Vector3(component4.localScale.x, component4.localScale.y, 1f);
	}

	// create a function to toggle the animation on button click
	public void ToggleAnimation()
	{
		if (toggleFlag) ReverseCoupling();
		else ForwardCoupling();
		toggleFlag = !toggleFlag;
	}

	// create a function to couple the components by executing the animations in sequence
	public void ForwardCoupling() => CoupleComponents();
	private void CoupleComponents()
	{
		if (!btnStateFlag) return; // prevent multiple clicks on button while animation is running
		btnStateFlag = false;


		// move component0 in Z from 5.9f to 0f
		PrimeTween.Tween.LocalPositionZ(component0, 0f, duration).OnComplete(FixSlots);
	}
	private void FixSlots()
	{
		// rotate component1 in Z from 0f to 90f
		PrimeTween.Tween.LocalRotation(component1, new Vector3(0f, 0f, 90f), duration).OnComplete(() => btnStateFlag = true);

		// move component2 in Z from 0f to -23f
		PrimeTween.Tween.LocalPositionZ(component2, -23f, duration);

		// move component3 in Z from 0f to -23f
		PrimeTween.Tween.LocalPositionZ(component3, -23f, duration);

		// scale component4 in Z from 1f to 0.35f
		PrimeTween.Tween.Scale(component4, new Vector3(1f, 1f, 0.35f), duration);
	}

	// create a function to decouple the components by reveresing the animations
	public void ReverseCoupling() => UnFixSlots();
	private void UnFixSlots()
	{
		if (!btnStateFlag) return; // prevent multiple clicks on button while animation is running
		btnStateFlag = false;

		// rotate component1 in Z from 90f to 0f
		PrimeTween.Tween.LocalRotation(component1, new Vector3(0f, 0f, 0f), duration).OnComplete(DecoupleComponents);

		// move component2 in Z from -23f to 0f
		PrimeTween.Tween.LocalPositionZ(component2, 0f, duration);

		// move component3 in Z from -23f to 0f
		PrimeTween.Tween.LocalPositionZ(component3, 0f, duration);

		// scale component4 in Z from 0.35f to 1f
		PrimeTween.Tween.Scale(component4, new Vector3(1f, 1f, 1f), duration);
	}
	private void DecoupleComponents()
	{
		// move component0 in Z from 0f to 5.9f
		PrimeTween.Tween.LocalPositionZ(component0, 5.9f, duration).OnComplete(() => btnStateFlag = true);
	}
}