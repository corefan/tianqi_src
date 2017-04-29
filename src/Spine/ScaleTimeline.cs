using System;

namespace Spine
{
	public class ScaleTimeline : TranslateTimeline
	{
		public ScaleTimeline(int frameCount) : base(frameCount)
		{
		}

		public override void Apply(Skeleton skeleton, float lastTime, float time, ExposedList<Event> firedEvents, float alpha)
		{
			float[] frames = this.frames;
			if (time < frames[0])
			{
				return;
			}
			Bone bone = skeleton.bones.Items[this.boneIndex];
			if (time >= frames[frames.Length - 3])
			{
				bone.scaleX += (bone.data.scaleX * frames[frames.Length - 2] - bone.scaleX) * alpha;
				bone.scaleY += (bone.data.scaleY * frames[frames.Length - 1] - bone.scaleY) * alpha;
				return;
			}
			int num = Animation.binarySearch(frames, time, 3);
			float num2 = frames[num - 2];
			float num3 = frames[num - 1];
			float num4 = frames[num];
			float num5 = 1f - (time - num4) / (frames[num + -3] - num4);
			num5 = base.GetCurvePercent(num / 3 - 1, (num5 >= 0f) ? ((num5 <= 1f) ? num5 : 1f) : 0f);
			bone.scaleX += (bone.data.scaleX * (num2 + (frames[num + 1] - num2) * num5) - bone.scaleX) * alpha;
			bone.scaleY += (bone.data.scaleY * (num3 + (frames[num + 2] - num3) * num5) - bone.scaleY) * alpha;
		}
	}
}
