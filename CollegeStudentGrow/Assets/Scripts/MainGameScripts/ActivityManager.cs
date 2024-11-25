using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class ActivityManager : MonoBehaviour
{
        public TMP_Text coinText, stressText, dateText, levelText, gradeText, scoreText;
        public GameObject countdownPanel;
        public TMP_Text countdownText;
        public Button[] actionButtons;
        public Button storeButton;

        // Ȱ���� �̹��� ����
        public GameObject schoolImage;
        public GameObject tripImage;
        public GameObject restImage;
        public GameObject partTimeJobImage;

        private int activityCount = 0;

        private void Start()
        {
            countdownPanel.SetActive(false);

            // �̹��� �ʱ� ��Ȱ��ȭ
            schoolImage.SetActive(false);
            tripImage.SetActive(false);
            restImage.SetActive(false);
            partTimeJobImage.SetActive(false);

            UpdateUI();
        }

        public void GoToSchool()
        {
            Debug.Log("�б� ���� ���õ�");
            ModifyPlayerData(stress: 10, score: 5);
            StartCoroutine(ShowCountdownPanelWithImage(schoolImage));
        }

        public void DoPartTimeJob()
        {
            Debug.Log("�˹��ϱ� ���õ�");
            ModifyPlayerData(coin: 20, stress: 10);
            StartCoroutine(ShowCountdownPanelWithImage(partTimeJobImage));
        }

        public void GoOnTrip()
        {
            Debug.Log("����� ���õ�");
            ModifyPlayerData(coin: -15, stress: -5);
            StartCoroutine(ShowCountdownPanelWithImage(tripImage));
        }

        public void Rest()
        {
            Debug.Log("�޽��ϱ� ���õ�");
            ModifyPlayerData(stress: -10);
            StartCoroutine(ShowCountdownPanelWithImage(restImage));
        }

        private void ModifyPlayerData(int coin = 0, int stress = 0, int score = 0)
        {
            var player = DataManager.instance.nowPlayer;

            player.coin = Mathf.Max(0, player.coin + coin);
            player.stress = Mathf.Max(0, player.stress + stress);
            player.score += score;

            CompleteActivity();
        }

        private IEnumerator ShowCountdownPanelWithImage(GameObject activityImage)
        {
            SetButtonsInteractable(false);
            countdownPanel.SetActive(true);

            // �̹��� ǥ��
            activityImage.SetActive(true);

            int countdown = 5;
            while (countdown > 0)
            {
                countdownText.text = countdown.ToString();
                yield return new WaitForSeconds(1);
                countdown--;
            }

            // �̹��� �����
            activityImage.SetActive(false);

            countdownPanel.SetActive(false);
            SetButtonsInteractable(true);
        }

        private void SetButtonsInteractable(bool interactable)
        {
            foreach (var button in actionButtons)
            {
                button.interactable = interactable;
            }
            storeButton.interactable = interactable;
        }

        private void CompleteActivity()
        {
            activityCount++;

            if (activityCount >= 2)
            {
                DateUp();
                activityCount = 0; // Ȱ�� ī��Ʈ �ʱ�ȭ
            }

            UpdateUI();
            DataManager.instance.SaveData();
        }

        private void DateUp()
        {
            DataManager.instance.nowPlayer.date++;
            Debug.Log("DateUp() ȣ���, ���� ��¥: " + DataManager.instance.nowPlayer.date);

            var endingManager = FindObjectOfType<EndingManager>();
            if (endingManager != null)
            {
                endingManager.CheckEnding();
            }
        }

        private void UpdateUI()
        {
            var player = DataManager.instance.nowPlayer;

            player.grade = CalculateGrade(player.score);

            coinText.text = $"Coin: {player.coin}";
            stressText.text = $"Stress: {player.stress}";
            dateText.text = $"{player.date}";
            levelText.text = $"Level: {player.level}";
            gradeText.text = $"Grade: {player.grade}";
            scoreText.text = $"Score: {player.score}";
        }

        private string CalculateGrade(int score)
        {
            if (score >= 95) return "A+";
            if (score >= 85) return "A";
            if (score >= 75) return "B+";
            if (score >= 65) return "B";
            if (score >= 55) return "C+";
            if (score >= 45) return "C";
            if (score >= 35) return "D+";
            if (score >= 25) return "D";
            return "F";
        }

        public int GetActivityCount()
        {
            return activityCount;
        }

}
