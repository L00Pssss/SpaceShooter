using UnityEngine;

namespace SpaceShooter
{
    public class AnimationSpriteColor : AnimationBase
    {
        [SerializeField] private SpriteRenderer m_Renderer;

        [SerializeField] private Color ColorA;

        [SerializeField] private Color ColorB;

        [SerializeField] private AnimationCurve m_Curve;

        public override void PrepareAnimation()
        {

        }

        protected override void AnimateFrame()
        {
            m_Renderer.color = Color.Lerp(ColorA, ColorB, m_Curve.Evaluate(NormolizedAnimationTime));


        }

        protected override void OnAnimationEnd()
        {

        }

        protected override void OnAnimationStart()
        {

        }
    }

}