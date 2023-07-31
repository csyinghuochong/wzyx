namespace ET
{

    public class MergeZone_OnMergeZone : AEvent<EventType.MergeZone>
    {
        protected override void Run(EventType.MergeZone args)
        {
            RunAsync(args).Coroutine();
        }

        private async ETTask RunAsync(EventType.MergeZone args)
        {
            int oldzone = args.oldzone;
            int newzone = args.newzone;
            Log.Console("合区开始！");
            await MergeZoneHelper.MergeZone(oldzone, newzone);
            Log.Console("合区完成！");
        }
    }
}
