# AsteroidsArcade
simple game about asteroids

Инструкция пользователя
Классическая аркадная игра про астероиды в космосе.
Цель игры: уничтожать астероиды и корабли пришельцев, зарабатывая очки. У игрока есть 3 жизни.
Управление: клавиши WASD, стрельба : ЛКМ или "Пробел"

Инструкция программиста:
GameController: - скрипт отвечающий за геймплей
public Text scoreText; -- счёт
public Text livesText; -- жизни
public Text waveText; -- уровень
public Text hiscoreText; -- лучший счёт
SpawnAsteroids() - сначала уничтожает существующие астероиды, потом в цикле ( итерации идут до числа asteroidsRemaining = уровень * число увеличения астероидов каждый уровень) создаются новые астероиды.
Вычисляется случайная точка на сфере, добавляет к ней вектор длины, и для каждого астероида задается скорость, сила по оси x и по оси y.
IncrementScore()- ведет счет  и сохраняет его. Если астероиды все уничтожены, увеличивает уровень на 1 и создает новые астероиды.

DecrementLives() - ведет счёт кол-ва жизней, вызывается при столкновении с другими объектами. Уничтожает существующие корабли пришельцев. если жизней меньше 1, начинает новую игру.

DecrementAsteroids() уменьшает кол-во следующих астероидов

SplitAsteroid() разбивает 1 большой астероид на 3 маленьких

DestroyExistingAsteroids()- уничтожает астероиды по тегу

ShipController-скрипт управления кораблем

ControlRocket() задаем управление кораблем по горизонтали и вертикали, добавляем скорость
Shoot()  - создает объект пули, делает его активным. Воспроизводим аудиофайл выстрела
void OnTriggerEnter2D(Collider2D c) – при столкновении с любым объектом, кроме пули, перемещаем корабль в центр экрана, устанавливаем скорость в 0, уменьшаем жизнь.

AsteroidContoller – скрипт управления астероидами
Start() При создании определяем случайную скорость, угол вращения.
Update() –устанавливаем угол вращения. И задаем скорость.
OnCollisionEnter2D() проверяем на столкновение. Если большой астероид, тогда он уничтожается, объект столкновения тоже уничтожается Если маленький - уничтожается. Создается 3 маленьких астероида. Воспроизводится звук разрешения астероида. Увеличиваем счёт.

BulletController – скрипт управления пулями
Start()- задаем силу в направлении оси Y.
KillOldBullet() – уничтожаем созданные пули через 2 секунды.

EnemyShipController – скрипт управления кораблем пришельцев
Update – здесь получается ссылка на объект игрока (наш корабль). Если объект не найдет пробуем сделать это в следующем фрейме.
RotationToPlayer() -  вычисляется расстояние между кораблем игрока и кораблём пришельцев. Высчитывается арктангенс, задаем направление движение корабля пришельцев с поворотом в сторону корабля игрока.
MoveToPlayer() выполняем перемещение объект корабля до корабля игрока

EnemySpawner – скрипт появления кораблей пришельцев.
Вражеские корабли появляется через промежуток времени, определенной по формуле enemyRate *= 0.9f, где enemyRate – время появления первого корабля. Из которого вычитается время фрейма. Вычисляется случайная точна на сфере, к ней добавляется длина, в создается объект.

EuclideanTorus- Код просто проверяет, оставлено ли положение игрового объекта, к которому он прикреплен, экрана слева, справа, сверху или снизу. Если это так, он помещает его обратно на противоположную сторону.

