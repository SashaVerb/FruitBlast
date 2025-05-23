using System.Collections;
using System.Collections.Generic;

public class PerkBall : Ball
{
    public override bool TryPop(out List<Ball> ballsToDestroy)
    {
        ballsToDestroy = new List<Ball>() {this};
        return true;
    }

    protected override IEnumerator DestroyEffect()
    {
        yield return bubble.PlayEffect();
    }
}
