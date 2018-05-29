<?php

use Phinx\Db\Adapter\MysqlAdapter;
use Phinx\Db\Adapter\PostgresAdapter;
use Phinx\Migration\AbstractMigration;

class CreateUsersTable extends AbstractMigration
{
    public function change()
    {
        $table = $this->table('users');
        $table->addColumn('login', 'string')
            ->addColumn('password', 'string')
            ->addColumn('register_date', 'timestamp', ['default' => 'CURRENT_TIMESTAMP'])
            ->create();
    }
}
