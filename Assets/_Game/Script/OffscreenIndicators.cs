using System;
using MEC;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Nếu bạn sử dụng Text
using TMPro;  // Nếu sử dụng TextMeshPro

namespace Custom.Indicators
{
    [RequireComponent(typeof(Canvas))]
    public class OffscreenIndicators : MonoBehaviour
    {
        public List<Indicator> targetIndicators;
        public GameObject indicatorPrefab;
        public float checkTime = 0.1f;
        public Vector2 offset;
        public float verticalOffset = 2f;

        private Camera activeCamera;
        private Transform _transform;
        private void Start()
        {
            activeCamera = Camera.main;
            _transform = transform;
            Timing.RunCoroutine(UpdateIndicators().CancelWith(gameObject));
        }

        public void AddTarget(Character targetObject)
        {
            // Instantiate indicatorPrefab và thiết lập scoreText cho từng enemy
            Transform indicatorTransform = Instantiate(indicatorPrefab,transform).transform;
            var scoreText = indicatorTransform.GetComponentInChildren<TextMeshProUGUI>(); // Hoặc Text nếu không dùng TMP

            Indicator newIndicator = new Indicator()
            {
                target = targetObject.transform,
                indicatorUI = indicatorTransform,
                rectTranForm = indicatorTransform.GetComponent<RectTransform>(),
                scoreText = scoreText
            };

            if (newIndicator.scoreText != null)
            {
                newIndicator.scoreText.text = targetObject.score.ToString(); // Hiển thị điểm của 
            }

            targetIndicators.Add(newIndicator);
        }

        public void RemoveTarget(GameObject targetObject)
        {
            var targetIndicator = targetIndicators.Find(indicator => indicator.target == targetObject.transform);

            if (targetIndicator != null)
            {
                if (targetIndicator.indicatorUI != null)
                {
                    Destroy(targetIndicator.indicatorUI.gameObject);
                }

                targetIndicators.Remove(targetIndicator);
            }
        }

        private void UpdatePosition(Indicator targetIndicator)
        {
            var rect = targetIndicator.rectTranForm.rect;
            var indicatorPosition = activeCamera.WorldToScreenPoint(targetIndicator.target.position);

            if (indicatorPosition.z < 0)
            {
                indicatorPosition.y = -indicatorPosition.y;
                indicatorPosition.x = -indicatorPosition.x;
            }

            // Dịch vị trí lên theo trục Y
            indicatorPosition.y += verticalOffset;

            var newPosition = new Vector3(indicatorPosition.x, indicatorPosition.y, indicatorPosition.z);
            indicatorPosition.x = Mathf.Clamp(indicatorPosition.x, rect.width / 2, Screen.width - rect.width / 2) + offset.x;
            indicatorPosition.y = Mathf.Clamp(indicatorPosition.y, rect.height / 2, Screen.height - rect.height / 2) + offset.y;
            indicatorPosition.z = 0;

            targetIndicator.indicatorUI.up = (newPosition - indicatorPosition).normalized;
            targetIndicator.indicatorUI.position = indicatorPosition;
            targetIndicator.indicatorUI.rotation = Quaternion.identity;
        }

        private IEnumerator<float> UpdateIndicators()
        {
            while (true)
            {
                foreach (var targetIndicator in targetIndicators)
                {
                    // Cập nhật điểm số mới nhất từ đối tượng kẻ địch
                    var character = targetIndicator.target.GetComponent<Character>();
                    if (character != null && targetIndicator.scoreText != null)
                    {
                        targetIndicator.scoreText.text = character.score.ToString();
                    }

                    // Cập nhật vị trí indicator
                    UpdatePosition(targetIndicator);
                }
                yield return Timing.WaitForSeconds(checkTime);
            }
        }
    }

    [System.Serializable]
    public class Indicator
    {
        public Transform target;
        public Transform indicatorUI;
        public RectTransform rectTranForm;
        public TextMeshProUGUI scoreText;  // Thêm Text để hiển thị điểm
    }
}
