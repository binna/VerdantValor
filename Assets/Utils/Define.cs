namespace Knight
{
    public static class Define
    {
        public enum ItemType
        {
            PotionHp,
            PotionAtk,
            Gold
        }

        public enum SceneType
        {
            Town = 0,
            Adventure,
            Intro
        }

        public enum SoundType
        {
            Bgm,
            Event
        }

        public enum UiName
        {
            HUD,
            Loading,
            Fade,
            Intro,
            Sign,
            ShopNpc,
            Shop,
            Setting,
            Inventory,
            Alarm
        }
        
        public enum Tag
        {
            Untagged,
            Respawn,
            Finish,
            EditorOnly,
            MainCamera,
            Player,
            GameController,
            Ladder
        }

        public enum Layer : int
        {
            Ground = 6,
            Player,
            Item,
            Monster
        }
        
        public static class GameObjectNames
        {
            public const string SOUND_MANAGER = "@SoundManager";
            public const string BGM_AUDIO = "Bgm Audio";
            public const string EVENT_AUDIO = "Event Audio";
        }
        
        public static class UiObjectNames
        {
            public const string SLIDER_BGM_VOLUME = "Slider_BgmVolume";
            public const string TOGGLE_BGM_MUTE = "Toggle_BgmMute";
            
            public const string SLIDER_EVENT_VOLUME = "Slider_EventVolume";
            public const string TOGGLE_EVENT_MUTE = "Toggle_EventMute";
            
            public const string IMG_PROGRESS_BAR = "Img_ProgressBar";
            public const string IMG_BACKGROUND = "Img_Background";
            
            public const string EXIT_BUTTON = "Btn_Exit";
            public const string ENTER_BUTTON = "Btn_Enter";
            
            public const string IMG_ICON = "Img_Icon";
            
            public const string TXT_ID = "Txt_Id";
            public const string TXT_LEVEL = "Txt_Level";
            public const string TXT_GOLD = "Txt_Gold";
        }
        
        // Sounds
        public const string INTRO_BGM_PATH = "Sounds/IntroBGM";
        public const string TOWN_BGM_PATH = "Sounds/TownBGM";
        public const string ADVENTURE_BGM_PATH = "Sounds/AdventureBGM";
        public const string PORTAL_PATH = "Sounds/Portal";
        
        // speed
        public const float DIALOG_TYPING_SPEED = 0.05f;
        
        // Path
        public const string IMAGES_PATH = "Images/";
        public const string PREFABS_PATH = "Prefabs/";

        // Use
        public const int INVNETORY_COUNT = 20;
        public const int RECOVERY_HP = 1;
    }
}
