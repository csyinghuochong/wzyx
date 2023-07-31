namespace ET
{
    public class GenerateSerials_OnGenerateSerials : AEvent<EventType.GenerateSerials>
    {
        protected override void Run(EventType.GenerateSerials args)
        {
            //20230070707 1     ->第五批序列号1
            Log.Console($"生成序列号: {args.AccountCenterScene.DomainZone()}");
            args.AccountCenterScene.GetComponent<AccountCenterComponent>().GenerateSerials(6);
        }
    }
}
