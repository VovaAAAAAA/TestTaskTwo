using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Enemy _enemy;
    private Controller _player;
    private СubeRotate[] _cubeRotate;
    private EdgeAbility[] _edgeAbility = new EdgeAbility[6];

    [SerializeField] private Text _infoText;

    private bool _moveEnemy;
    private bool _movePlayer;
    private bool _clickPlayer;
    private bool _clickEnemy;
    private bool _getNum;
    private bool _doAbility;
    private bool _checkComboAttack;
    private bool _corutinaStop;

    private int _countAttack;
    private int _countAttackKnife;
    private int _countTreatment;
    private int _countArmor;

    void Awake()
    {
        _enemy = FindObjectOfType<Enemy>();
        _player = FindObjectOfType<Controller>();
        _moveEnemy = true;
        _movePlayer = false;

        _clickEnemy = true;
        _clickPlayer = false;

        _cubeRotate = FindObjectsOfType<СubeRotate>();
    }

    void Update()
    {
        if (_movePlayer)
        {
            if (_clickPlayer)
            {
                SetText("Ваш ход");
                RotateAllTheCube();
            }
            else if (_getNum)
            {
                StartCoroutine(CorutinaWaiting());
            }

            if (_checkComboAttack && _corutinaStop)
            {
                SetText("Вы атакуете");
                if (!_player.GetCorrectPoint)
                    _player.Move();
                else
                {
                    _player.Retrect();
                    if (!_player.GetCorrectPoint)
                    {
                        ResetEverything();
                    }
                }
            }
            else if (!_checkComboAttack && _corutinaStop)
            {
                ResetEverything();
            }
        }

        if (_moveEnemy)
        {
            if (_clickEnemy)
            {
                SetText("Ход врага");
                RotateAllTheCube();
            }
            else if (_getNum)
            {
                StartCoroutine(CorutinaWaiting());
            }

            if (_checkComboAttack && _corutinaStop)
            {
                SetText("Враг атакует");
                if (!_enemy.GetCorrectPoint)
                    _enemy.Move();
                else
                {
                    _enemy.Retrect();
                    if (!_enemy.GetCorrectPoint)
                    {
                        NextMovePlayer();
                    }
                }
            }
            else if(!_checkComboAttack && _corutinaStop) 
            {
                NextMovePlayer();
            }

        }
    }

    private void NextMovePlayer()
    {
        SetText("Нажмите, что бы походить");
        if (Input.GetMouseButtonUp(0))
        {
            ResetEverything();
        }
    }

    public void ResetEverything()
    {
        _moveEnemy = !_moveEnemy;
        _movePlayer = !_movePlayer;

        if (_moveEnemy)
        {
            _clickEnemy = true;
        }
        if (_movePlayer)
        {
            _clickPlayer = true;
        }

        _getNum = false;
        _doAbility = false;
        _checkComboAttack = false;
        _corutinaStop = false;

        _countAttack = 0;
        _countAttackKnife = 0;
        _countTreatment = 0;
        _countArmor = 0;

        _player.Damage = 0;
        _enemy.Damage = 0;
    }

    private void RotateAllTheCube()
    {
        for (int i = 0; i < _cubeRotate.Length; i++)
        {
            _cubeRotate[i].RotateCube();
        }
        _clickPlayer = false;
        _clickEnemy = false;
        _getNum = true;
    }    
    private void GetNumAllTheCube()
    {
        for (int i = 0; i < _cubeRotate.Length; i++)
        {
            if(_cubeRotate[i].GetNum() != null)
                _edgeAbility[i] = _cubeRotate[i].GetNum();
        }
        _getNum = false;
        _doAbility = true;
    }

    private void SetText(string text)
    {
        _infoText.text = text;
    }

    private IEnumerator CorutinaWaiting()
    {
        _corutinaStop = false;
        yield return new WaitForSeconds(2);

        if(_getNum)
            GetNumAllTheCube();

        if (_moveEnemy)
        {
            SetText("Подсчeт комбинаций противника");
            if(_doAbility)
                CalculateAbily(_enemy);
        }
        else if (_movePlayer)
        {
            SetText("Подсчет ваших комбинаций");
            if(_doAbility)
                CalculateAbily(_player);
        }
        yield return new WaitForSeconds(2);
        _corutinaStop = true;
    }

    private void CalculateAbily(Controller player)
    {
        for(int i = 0; i < _edgeAbility.Length; i++)
        {
            if (_edgeAbility[i] is EdgeAttack)
            {
                _countAttack += 1;
            }            
            if (_edgeAbility[i] is EdgeAttackKnife)
            {
                _countAttackKnife += 1;
            }            
            if (_edgeAbility[i] is EdgeTreatment)
            {
                _countTreatment += 1;
            }
            if (_edgeAbility[i] is EdgeArmor)
            {
                _countArmor += 1;
            }
        }

        if (_countAttack >= 3)
        {
            for (int i = 0; i < _edgeAbility.Length; i++)
            {
                if (_edgeAbility[i] is EdgeAttack)
                {
                    _edgeAbility[i].Ability(player);
                    _checkComboAttack = true;
                }
            }
        }        
        if (_countAttackKnife >= 3)
        {
            for (int i = 0; i < _edgeAbility.Length; i++)
            {
                if (_edgeAbility[i] is EdgeAttackKnife)
                {
                    _edgeAbility[i].Ability(player);
                    _checkComboAttack = true;
                }
            }
        }        
        if (_countTreatment >= 3)
        {
            for (int i = 0; i < _edgeAbility.Length; i++)
            {
                if (_edgeAbility[i] is EdgeTreatment)
                {
                    _edgeAbility[i].Ability(player);
                }
            }
        }
        _doAbility = false;
        StopCoroutine(CorutinaWaiting());
    }    
    private void CalculateAbily(Enemy enemy)
    {
        for(int i = 0; i < _edgeAbility.Length; i++)
        {
            if (_edgeAbility[i] is EdgeAttack)
            {
                _countAttack += 1;
            }            
            if (_edgeAbility[i] is EdgeAttackKnife)
            {
                _countAttackKnife += 1;
            }            
            if (_edgeAbility[i] is EdgeTreatment)
            {
                _countTreatment += 1;
            }
            if (_edgeAbility[i] is EdgeArmor)
            {
                _countArmor += 1;
            }
        }

        if (_countAttack >= 3)
        {
            for (int i = 0; i < _edgeAbility.Length; i++)
            {
                if (_edgeAbility[i] is EdgeAttack)
                {
                    _edgeAbility[i].Ability(enemy);
                    _checkComboAttack = true;
                }
            }
        }        
        if (_countAttackKnife >= 3)
        {
            for (int i = 0; i < _edgeAbility.Length; i++)
            {
                if (_edgeAbility[i] is EdgeAttackKnife)
                {
                    _edgeAbility[i].Ability(enemy);
                    _checkComboAttack = true;
                }
            }
        }        
        if (_countTreatment >= 3)
        {
            for (int i = 0; i < _edgeAbility.Length; i++)
            {
                if (_edgeAbility[i] is EdgeTreatment)
                {
                    _edgeAbility[i].Ability(enemy);
                }
            }
        }
        _doAbility = false;
        StopCoroutine(CorutinaWaiting());
    }
}
