using UnityEngine;

public class TrenchProtectionZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(
            "TRIGGER DETECTADO! Entrou: " + other.gameObject.name
        );

        SoldierHealth soldier =
            other.GetComponent<SoldierHealth>();

        if (soldier != null)
        {
            soldier.SetInsideTrench(true);

            Debug.Log(
                "SOLDADO ENTROU NA TRINCHEIRA! Proteção ativada."
            );
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(
            "TRIGGER SAÍDA! Saiu: " + other.gameObject.name
        );

        SoldierHealth soldier =
            other.GetComponent<SoldierHealth>();

        if (soldier != null)
        {
            soldier.SetInsideTrench(false);

            Debug.Log(
                "SOLDADO SAIU DA TRINCHEIRA! Soldado exposto."
            );
        }
    }
}