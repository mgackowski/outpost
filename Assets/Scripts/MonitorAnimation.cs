using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MonitorAnimation : MonoBehaviour
{
	public Sprite[] stableSprites;
	public Sprite[] unconsciousSprites;
	public Sprite[] convulsingSprites;
	public Sprite[] flatLiningSprites;
	public Sprite[] dyingSprites;
	public Sprite[] zombifyingSprites;

	private Sprite[] selectedSprites;

	public int spritePerFrame = 6;
	public bool loop = true;

	private int index = 0;
	private Image image;
	private int frame = 0;

	void Awake()
	{
		image = GetComponent<Image>();

		selectedSprites = stableSprites;
		System.Random rnd = new System.Random();
		index = rnd.Next(0, stableSprites.Length);
	}

	void Update()
	{
		if (!loop && index == selectedSprites.Length) return;
		frame++;
		if (frame < spritePerFrame) return;
		image.sprite = selectedSprites[index];
		frame = 0;
		index++;
		if (index >= selectedSprites.Length)
		{
			if (loop) index = 0;
		}

	}

	public void ChangeAnimation(NPC.NPCState state)
    {
        switch (state)
        {
            case NPC.NPCState.Incapacitated:
				selectedSprites = stableSprites;
				loop = true;
				spritePerFrame = 40;
                break;
            case NPC.NPCState.Unconscious:
				selectedSprites = unconsciousSprites;
				loop = false;
				spritePerFrame = 200;
				break;
            case NPC.NPCState.Convulsing:
				selectedSprites = convulsingSprites;
				loop = true;
				spritePerFrame = 60;
				break;
            case NPC.NPCState.Flatlining:
				selectedSprites = flatLiningSprites;
				loop = true;
				spritePerFrame = 60;
				break;
            case NPC.NPCState.Dead:
				selectedSprites = dyingSprites;
				loop = false;
				spritePerFrame = 40;
				break;
            case NPC.NPCState.Zombie:
				selectedSprites = zombifyingSprites;
				loop = false;
				spritePerFrame = 60;
				break;
        }
		index = 0;

    }
}
