using System.Threading.Tasks;
using UnityEngine;

namespace ThroneRoom.Scripts
{
    public class ThroneRoomPlayer : MonoBehaviour
    {

        [SerializeField] private Transform GFX;
        private Animator _animator;

        public bool PlayingAnimation;
    
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void SavePosition()
        {
            _animator.enabled = false;
            transform.position = GFX.position;
            GFX.localPosition = Vector3.zero;
            PlayingAnimation = false;
        }

        public async Task AdvanceStep()
        {
            _animator.enabled = true;
            PlayingAnimation = true;
            _animator.SetTrigger("Advance_Step");

            while (PlayingAnimation)
            {
                await Task.Yield();
            }
        }

        public void SetToStep(int points)
        {
            transform.position += new Vector3(0, 0.2f, 0.3f) * points;
        }
    }
}
