namespace Project.EmergingObjectsAndPlayer.Scripts
{
    public class Coin : AbstractObject
    {

        public void OnTaken()
        {
            
            gameObject.SetActive(false);
        }
    }
}
