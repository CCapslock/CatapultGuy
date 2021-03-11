using UnityEngine;

public class StringsController : MonoBehaviour
{
	public Transform LeftSlingShotPoint1;
	public Transform LeftSlingShotPoint2;
	public Transform RightSlingShotPoint1;
	public Transform RightSlingShotPoint2;

	public LineRenderer LeftRenderer;
	public LineRenderer RightRenderer;

	public Transform SlingShotBackPart;

	private void FixedUpdate()
	{
		ChargeSlingShot();
	}
	public void ChargeSlingShot()
	{
		LeftRenderer.SetPosition(0, LeftSlingShotPoint1.position);
		LeftRenderer.SetPosition(1, LeftSlingShotPoint2.position);
		RightRenderer.SetPosition(0, RightSlingShotPoint1.position);
		RightRenderer.SetPosition(1, RightSlingShotPoint2.position);
	}
}
