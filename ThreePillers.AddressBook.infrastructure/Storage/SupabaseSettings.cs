namespace ThreePillers.AddressBook.infrastructure.Storage;

public static class SupabaseSettings
{
    public static string SupabaseUrl => "https://hjrkjcfhkhesbbkkjoon.supabase.co/storage/v1";
    public static string SupabaseKey =>
        "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImhqcmtqY2Zoa2hlc2Jia2tqb29uIiwicm9sZSI6InNlcnZpY2Vfcm9sZSIsImlhdCI6MTc1NjM4MDczMCwiZXhwIjoyMDcxOTU2NzMwfQ.LXSd-Vu1UqC0eggNt957CnJsm0qSW3RL-NJWSFHLFO0";
    public static string BaseBucket => "3pillars";
    public static string LimitSize => "50mb";
}
