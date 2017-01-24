#pragma strict
#pragma downcast
private
var _counter:float;
private
var _counterWait:float;
private
var _idle:boolean;
var names:String[];

function Start () {
names = GetAnimationNames(this.GetComponent.<Animation>());
GetComponent.<Animation>().Play("idleCasual");
}

function Update () {
	_counter+=Time.deltaTime;
	
	if(Input.GetKeyUp("1")){
		_counter = 0;
		GetComponent.<Animation>().CrossFade(names[2]);
		_counterWait = 4;
		_idle = false;
	}else if(Input.GetKeyUp("2")){
		 GetComponent.<Animation>().CrossFade("hit",.2);
		 _counter = 0;
		 _counterWait = 1;
		 _idle = false;
	}else if(Input.GetKeyUp("3")){
		GetComponent.<Animation>().CrossFade("angry");
		_counter = 0;
		_counterWait = 5;
		_idle = false;
	}else if(Input.GetKeyUp("4")){
		GetComponent.<Animation>().CrossFade("jawn");
		_counter = 0;
		_counterWait = 3;
		_idle = false;
	}else if(Input.GetKeyUp("5")){
		GetComponent.<Animation>().CrossFade("smile");
		_counter = 0;
		_counterWait = 2;
		_idle = false;
	}else if(Input.GetKeyUp("6")){
		GetComponent.<Animation>().CrossFade("death");
		_counter = 0;
		_counterWait = 3;
		_idle = false;
	}	
	
	if(_counter > _counterWait && !_idle){
		GetComponent.<Animation>().CrossFade("idleCasual");
		_idle = true;
	}
}

function GetAnimationNames(anim:Animation){
	var tmpList : Array = new Array(); 
	for (var state : AnimationState in anim) {
		tmpList.Add(state.name);
	}
	var list : String[] = tmpList.ToBuiltin(String);
	return list;
}
