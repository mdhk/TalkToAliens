#pragma strict

var wobble:float = 0.01;
var wobbleSpeed:float = 3;




function Update () {
	transform.localRotation.x = ((Mathf.Sin((Time.time) * wobbleSpeed) - wobble*.5) )*wobble;
	transform.localRotation.z = ((Mathf.Cos((Time.time) * wobbleSpeed) - wobble) )*wobble*2;
}