function Awake()
	print("lua awake...")
end

function Start()
	print("lua start...")
end

function Update()
    -- print("lua update...")
	print(CS.UnityEngine.Time.deltaTime)
end

function OnDestroy()
    print("lua destroy...")
end